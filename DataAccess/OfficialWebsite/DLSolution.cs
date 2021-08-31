using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DLSolution : DaoBase
    {
        /// <summary>
        /// 取得解决方案列表
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<V_Solution> GetSolutionList(string strKey,string status)
        {
            List<V_Solution> lst = new List<V_Solution>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.AppendLine("solution_id");
            sql.AppendLine(" , solution_name");
            sql.AppendLine(" , solution_summary");
            sql.AppendLine(" , solution_detailed_description");
            sql.AppendLine(" , solution_big_image");
            sql.AppendLine(" , solution_small_image");
            sql.AppendLine(" , solution_create_user");
            sql.AppendLine(" , solution_create_time");
            sql.AppendLine(" , solution_update_user");
            sql.AppendLine(" , solution_update_time");
            sql.AppendLine(" , isnull(t2.description, t1.solution_status) solution_status");
            sql.AppendLine(" , solution_point");
            sql.AppendLine(" from ow_solution t1");
            sql.AppendLine(" left join u_systeminfo t2 on t2.sysid = 1002");
            sql.AppendLine(" and t2.sysubid <> 0");
            sql.AppendLine(" and t2.sysvalue = t1.solution_status");
            sql.AppendLine(" where 1=1 ");
            if (!string.IsNullOrEmpty(strKey)) {
                sql.AppendLine(" and t1.solution_name like " + this.GetLikeSqlValueString(strKey));
            }
            if (!string.IsNullOrEmpty(status))
            {
                sql.AppendLine(" and t1.solution_status = " + this.GetSqlValueString(status));
            }
            sql.AppendLine(" order by  solution_point desc");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            return lst;
        }
        /// <summary>
        /// 根据主键取得解决方案
        /// </summary>
        /// <param name="solutionId"></param>
        /// <returns></returns>
        public V_Solution GetSolutionById(string solutionId)
        {
            List<V_Solution> lst = new List<V_Solution>();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.AppendLine("solution_id");
            sql.AppendLine(" , solution_name");
            sql.AppendLine(" , solution_summary");
            sql.AppendLine(" , solution_detailed_description");
            sql.AppendLine(" , solution_big_image");
            sql.AppendLine(" , solution_small_image");
            sql.AppendLine(" , solution_create_user");
            sql.AppendLine(" , solution_create_time");
            sql.AppendLine(" , solution_update_user");
            sql.AppendLine(" , solution_update_time");
            sql.AppendLine(" , isnull(t2.description, t1.solution_status) solution_status");
            sql.AppendLine(" , solution_point");
            sql.AppendLine(" from ow_solution t1");
            sql.AppendLine(" left join u_systeminfo t2 on t2.sysid = 1002");
            sql.AppendLine(" and t2.sysubid <> 0");
            sql.AppendLine(" and t2.sysvalue = t1.solution_status");
            sql.AppendLine(" where 1=1 ");
            sql.AppendLine(" and t1.solution_id = " + this.GetSqlValueString(solutionId));
            sql.AppendLine(" order by  solution_point desc");
            this.DataAccessClient.FillQuery(lst, sql.ToString());
            if (lst == null || lst.Count == 0)
            {
                return null;
            }
            return lst[0];
        }
    }
}
