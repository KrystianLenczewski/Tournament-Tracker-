using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connections { get; private set; }
        public static void InitalizeConnections(DatabaseType db)
        {
            
            if (db==DatabaseType.Sql)
            {
                SqlConnector sqlConnector = new SqlConnector();
                Connections = sqlConnector;
            }
            else if(db==DatabaseType.TextFile)
            {
                TextConnector text = new TextConnector();
                Connections = text;
            }
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }

}
