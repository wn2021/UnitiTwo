using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLTest : DaoBase
    {
        public List<V_tasks> GetTasks()
        {
            List<V_tasks> lst = new List<V_tasks>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select * from tasks where 1=1");
            
            this.DataAccessClient.FillQuery(lst, sql.ToString());

            return lst;
        }
    }
}
