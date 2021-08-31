using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eynaOA.Results
{
    public class User
    {

        public string USER_ID { get; set; }

        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string NAME { get; set; }
        public string ROLE_ID { get; set; }
        public string LAST_LOGIN { get; set; }
        public string IP { get; set; }
        public string STATUS { get; set; }
    }
}