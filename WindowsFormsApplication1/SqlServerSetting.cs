using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class SqlServerSetting
    {
        public string server { get; set; }
        public string uid { get; set; }
        public string pwd { get; set; }
        public string database { get; set; }
    }
    public class MySqlServerSetting
    {
        public string server { get; set; }
        public string port { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string database { get; set; }
    }
}
