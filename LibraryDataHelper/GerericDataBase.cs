using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace LibraryDataHelper
{
    public class GerericDataBase
    {

        public enum TypeCommand
        {
            ExecuteNonQuery
            , ExecuteReader
            , ExecuteScalar
            , ExecuteDataTable
        }

        public static DbCommand CreateCommand(string cmdText, CommandType cmmType, List<DbParameter> listParameters)
        {
            var factory = DbProviderFactories.GetFactory(ConnectionDB.ProviderName);

            var conn = factory.CreateConnection();

            conn.ConnectionString = ConnectionDB.Connectionstring;

            var comm = conn.CreateCommand();

            comm.CommandText = cmdText;

            comm.CommandType = cmmType;

            if (listParameters != null)
            {
                foreach (var param in listParameters)
                {
                    comm.Parameters.Add(param);
                }
            }

            return comm;
        }

        public static DbParameter CreateParameter(string nameParameter, DbType typeParameter, object valueParameter)
        {

            var factory = DbProviderFactories.GetFactory(ConnectionDB.ProviderName);

            var param = factory.CreateParameter();

            if (param != null)
            {
                param.ParameterName = nameParameter;
                param.DbType = typeParameter;
                param.Value = valueParameter;
            }

            return param;
        }

        public static object ExecuteCommand(string cmdText, CommandType cmdType, List<DbParameter> listParameters, TypeCommand typeCmd)
        {
            var command = CreateCommand(cmdText, cmdType, listParameters);
            object objRetorno = null;

            try
            {
                command.Connection.Open();

                switch (typeCmd)
                {
                    case TypeCommand.ExecuteNonQuery:
                        objRetorno = command.ExecuteNonQuery();
                        break;
                    case TypeCommand.ExecuteReader:
                        objRetorno = command.ExecuteReader();
                        break;
                    case TypeCommand.ExecuteScalar:
                        objRetorno = command.ExecuteScalar();
                        break;
                    case TypeCommand.ExecuteDataTable:
                        var table = new DataTable();
                        var reader = command.ExecuteReader();
                        table.Load(reader);
                        objRetorno = table;
                        break;
                }


            }
            catch (Exception)
            {

                throw new Exception("Erro ao execultar Execute Comando");
            }
            finally
            {
                if (typeCmd != TypeCommand.ExecuteReader)
                {
                    if (command.Connection.State == ConnectionState.Open)
                    {
                        command.Connection.Close();
                        command.Connection.Dispose();
                    }
                }

            }

            return objRetorno;
        }



    }
}
