using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataHelper
{
    public class ConnectionDB
    {

        public static string connectionString { get; set; }

        public static string providerName { get; set; }

        static ConnectionDB()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                providerName = ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao acessar connection String.");
            }

        }

        public static string Connectionstring
        {
            get { return connectionString; }

        }

        public static string ProviderName
        {
            get { return providerName; }

        }
    }
}
