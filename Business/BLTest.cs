using BFC.FRM.Dao;
using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLTest: BusinessBase
    {
        public List<V_tasks> GetTasks()
        {
            using (DaoTransactionScope ts = new DaoTransactionScope())
            {
                DLTest dl = new DLTest();
                List<V_tasks> result = dl.GetTasks();
                ts.Complete();
                return result;
            }
        }
        
    }
}
