using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Gas_Station
{
    static class DB
    {
        public static string stringConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\Resources\\gas_station.mdb";
        public static OleDbConnection myConnection = new OleDbConnection(stringConnection);
    }
}
