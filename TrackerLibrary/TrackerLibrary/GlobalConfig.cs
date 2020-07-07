using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();
        public static void InitalizeConnections(bool database, bool textFile)
        {
            if(database)
            {
                SqlConnector sqlConnector = new SqlConnector();
                Connections.Add(sqlConnector);
            }
            if(textFile)
            {
                TextConnection text = new TextConnection();
                Connections.Add(text);
            }
        }

    }
}
