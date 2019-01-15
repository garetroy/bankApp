using System.Collections.Generic;

namespace bankLedger.Data
{
    public class DataBase
    {
        public DataBase()
        {
            DB = new Dictionary<string, object>();
        }

        public Dictionary<string, object> DB { get; set; }
    }
}