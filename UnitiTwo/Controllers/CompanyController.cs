using Business;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitiTwo.Models;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        #region 公司
        [HttpGet]
        [Display(Description = "打开公司主画面")]
        public ActionResult Company()
        {
            return View();
        }
        [HttpGet]
        [Display(Description = "打开公司编辑画面")]
        public ActionResult CompanyEdit(string id)
        {
            u_company cmp = new BLCompany().GetCompanyById(id);
            if (cmp == null) { cmp = new u_company(); }
            return View(cmp);
        }

        [HttpPost]
        [Display(Description = "取得公司信息")]
        public ActionResult CompanyList(int? pageIndex, int? pageSize, CompanySearch companySearch)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;
            List<u_company> list = new List<u_company>();
            if (companySearch == null) { companySearch = new CompanySearch(); }
            List<u_company> lst = new BLCompany().GetCompanys(companySearch);
            if (lst != null && lst.Count > 0)
            {
                list = lst.OrderBy(d => d.uc_id).Skip((pageIndex * pageSize).Value)
          .Take((pageSize).Value).ToList();
            }
            var intCnt = list.Count;
            return Json(new { total = intCnt, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "保存公司信息")]
        public ActionResult CompanySave(u_company company)
        {
            if (company == null) { return Json("未取得要保存的数据"); }
            BLCompany bl = new BLCompany();
            string user = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            u_user uu = new BLUserInfo().GetUserByKeyValue(user);
            ActionType action = ActionType.Edit;
            if (string.IsNullOrEmpty(company.uc_id))
            {
                company.uc_id = bl.GetNextCompanyId();
                company.uc_status = "10";
                company.uc_create_time = DateTime.Now;
                company.uc_create_user = (uu == null) ? "1" : uu.UU_ID.ToString();
                if (company.uc_register_time != null) { company.uc_register_time = DateTime.Today; };
                action = ActionType.AddNew;
            }
            company.uc_update_time = DateTime.Now;
            company.uc_update_user = (uu == null) ? "1" : uu.UU_ID.ToString();
            List<u_company> lst = new List<u_company>();
            lst.Add(company);
            int intResult = bl.SaveCompany(lst, action);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "删除公司信息")]
        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids)) { return Json("未取得要删除的数据"); }
            string[] arr = ids.Split(',');
            BLCompany bl = new BLCompany();
            List<u_company> lst = new List<u_company>();
            u_company cmp = null;
            string user = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            u_user uu = new BLUserInfo().GetUserByKeyValue(user);
            for (int i = 0; i < arr.Length; i++) {
                cmp = bl.GetCompanyById(arr[i]);
                if (cmp == null) { continue; }
                cmp.uc_update_time = DateTime.Now;
                cmp.uc_update_user = (uu == null) ? "1" : uu.UU_ID.ToString();
                if (cmp.uc_status != "90" && cmp.uc_status != "00") {
                    cmp.uc_destroy_time = DateTime.Now;
                }
                cmp.uc_status = "90";
                lst.Add(cmp);
            }
            int intResult = bl.SaveCompany(lst, ActionType.Edit);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Display(Description = "展示公司简介")]
        public ActionResult CompanyProfile(string id)
        {
            u_company uu = new BLCompany().GetCompanyById(id);
            if (uu == null) { uu = new u_company(); }
            return View(uu);
        }

        [HttpPost]
        [Display(Description = "保存公司简介")]
        public ActionResult ProfileSave(string id, string profile)
        {
            BLCompany bl = new BLCompany();
            u_company uu = bl.GetCompanyById(id);
            if (uu == null) { return Json("未取得要删除的数据"); }
            profile = profile.Replace("br", "\r\n");
            uu.uc_description = profile;
            List<u_company> lst = new List<u_company>();
            lst.Add(uu);
            int intResult = bl.SaveCompany(lst, ActionType.Edit);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        #endregion 公司

        #region 部门信息
        [HttpGet]
        public ActionResult DepartPage() {
            return View();
        }
        [HttpPost]
        [Display(Description = "取得部门信息")]
        public ActionResult DepartmentList(int? pageIndex, int? pageSize, DepartmentSearch search)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            List<V_department> list = new List<V_department>();
            if (search == null) { search = new DepartmentSearch(); }
            List<V_department> lst = new BLDepartment().GetDepartments(search);
            if (lst == null) { lst = new List<V_department>(); }
            var intCnt = lst.Count;
            return Json(new { total = intCnt, data = lst }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompanySelect() {
            List<u_company> lst = new BLCompany().GetCompanys(new CompanySearch());
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }

        
        [HttpGet]
        [Display(Description = "编辑部门")]
        public ActionResult DepartEdit(string companyId, string depId, string actionType)
        {
            V_department dept = new V_department();

            if (string.IsNullOrEmpty(companyId))
            {
                return Json("未取得比要参数companyId", JsonRequestBehavior.AllowGet);
            }
            else if (actionType == "New")
            {
                dept.ud_company_id = companyId;
                return View(dept);
            }
            else if (string.IsNullOrEmpty(depId))
            {
                return Json("未取得比要参数depId", JsonRequestBehavior.AllowGet);
            }
            DepartmentSearch condtion = new DepartmentSearch();
            condtion.companyId = companyId;
            condtion.departmentId = depId;
            List<V_department> list = new BLDepartment().GetDepartments(condtion);
            if (list == null)
            {
                return Json("未取得对应的数据", JsonRequestBehavior.AllowGet);
            }
            dept = list[0];
            return View(dept);

        }
        [HttpPost]
        [Display(Description = "删除部门")]
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public ActionResult DepartDel(string companyId, string depId) {
            string user = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            BLDepartment bl = new BLDepartment();
            u_department dep = bl.GetDeptByKeyValue(companyId, depId);
            if (dep == null) { return Json("部门不存在", JsonRequestBehavior.AllowGet); }
            dep.ud_status = "9";
            dep.ud_update_time = DateTime.Now;
            dep.ud_update_user = user;
            List<u_department> lst = new List<u_department>();
            lst.Add(dep);
            int intResult = bl.UpdateDept(lst);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "保存部门")]
        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult SaveDept(u_department data) {
            if (data == null) { return View(); }
            ActionType action = ActionType.Edit;
            string user = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            BLDepartment bl = new BLDepartment();
            if (string.IsNullOrEmpty(data.ud_id)) {
                action = ActionType.AddNew;
                data.ud_id = bl.GetNextDepartId(data.ud_company_id);
                data.ud_create_time = DateTime.Now;
                data.ud_create_user = user;
                data.ud_status = "1";
            }
            data.ud_update_time = DateTime.Now;
            data.ud_update_user = user;
            List<u_department> lst = new List<u_department>();
            lst.Add(data);
            int intResult = -1;
            if (action == ActionType.Edit)
            {
                bl.UpdateDept(lst);
            }
            else { bl.InsertDept(lst); }           
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        #endregion 部门信息

        #region 职位级别
        [HttpPost]
        [Display(Description = "取得职级信息")]
        public ActionResult LevelList(int? pageIndex, int? pageSize, PositionLeveltSearch search)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            List<V_position_level> list = new List<V_position_level>();
            if (search == null) { search = new PositionLeveltSearch(); }
            List<V_position_level> lst = new BLDepartment().GetDeptPositionLevels(search);
            if (lst != null && lst.Count > 0)
            {
                list = lst.OrderBy(d => d.upl_id).Skip((pageIndex * pageSize).Value)
          .Take((pageSize).Value).ToList();
            }
            var intCnt = list.Count;
            return Json(new { total = intCnt, data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Display(Description = "编辑职级")]
        public ActionResult PositionLevelEdit(string companyId,string departmentId, string levelId ) {
            V_position_level level = new V_position_level();
            if (string.IsNullOrEmpty(companyId))
            {
                return Json("未取得比要参数companyId", JsonRequestBehavior.AllowGet);
            }
            else if (string.IsNullOrEmpty(departmentId))
            {
                return Json("未取得比要参数companyId", JsonRequestBehavior.AllowGet);
            }
            BLDepartment bl = new BLDepartment();
            if (string.IsNullOrEmpty(levelId))
            {
                DepartmentSearch condtion = new DepartmentSearch();
                condtion.companyId = companyId;
                condtion.departmentId = departmentId;
                List<V_department> lst = bl.GetDepartments(condtion);
                if (lst == null || lst.Count == 0) { return View(level); }
                else
                {
                    V_department dep = lst[0];
                    level.upl_company_id = dep.ud_company_id;
                    level.upl_department_id = dep.ud_id;
                    level.upl_company_name = dep.ud_company_name;
                    level.upl_department_name = dep.ud_name;
                }
            }
            else {
                PositionLeveltSearch search = new PositionLeveltSearch();
                search.companyId = companyId;
                search.departmentId = departmentId;
                search.levelId = levelId;
                List<V_position_level> lst =bl.GetDeptPositionLevels(search);
                if (lst == null || lst.Count == 0) { return View(level); }
                return View(lst[0]);
            }
            return View(level);
        }
        [HttpPost]
        [Display(Description = "删除职级")]
        public ActionResult PositionLevelDel(string companyId, string depId,string levelId)
        {
            string user = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            BLDepartment bl = new BLDepartment();
            u_position_level level = bl.GetLevelByKeyValue(companyId, depId, levelId);
            if (level == null) { return Json("部门不存在", JsonRequestBehavior.AllowGet); }
            level.upl_status = "9";
            level.upl_update_time = DateTime.Now;
            level.upl_update_user = user;
            List<u_position_level> lst = new List<u_position_level>();
            lst.Add(level);
            int intResult = bl.UpdateLevel(lst);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "保存职级")]
        public ActionResult SaveLevel(V_position_level data)
        {
            u_position_level pl = new u_position_level();
            if (data == null) { return View(); }
            ActionType action = ActionType.Edit;
            string user = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            BLDepartment bl = new BLDepartment();
            if (string.IsNullOrEmpty(data.upl_id))
            {
                action = ActionType.AddNew;
                pl.upl_id = bl.GetNextLevelId(data.upl_company_id, data.upl_department_id);
                pl.upl_create_time = DateTime.Now;
                pl.upl_create_user = user;
                pl.upl_status = "1";
                pl.upl_company_id = data.upl_company_id;
                pl.upl_department_id = data.upl_department_id;
            }
            else {
                pl.upl_id = data.upl_id;
                pl.upl_status = data.upl_status;
                pl.upl_company_id = data.upl_company_id;
                pl.upl_department_id = data.upl_department_id;
            }
            pl.upl_name = data.upl_name;
            pl.upl_update_time = DateTime.Now;
            pl.upl_update_user = user;
            List<u_position_level> lst = new List<u_position_level>();
            lst.Add(pl);
            int intResult = -1;
            if (action == ActionType.Edit)
            {
                bl.UpdateLevel(lst);
            }
            else { bl.InsertLevel(lst); }
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        #endregion 职位级别

        [HttpGet]
        [Display(Description = "打开员工管理主画面")]
        public ActionResult Employee()
        {
            return View();
        }

        [HttpPost]
        [Display(Description = "取得员工列表")]
        public ActionResult EmployeeList(int? pageIndex, int? pageSize, EmployeeSearch search)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            List<V_employee> list = new List<V_employee>();
            if (search == null) { search = new EmployeeSearch(); }
            List<V_employee> lst = new BLEmployee().GetEmployees(search);
            if (lst != null && lst.Count > 0)
            {
                list = lst.Skip((pageIndex * pageSize).Value)
          .Take((pageSize).Value).ToList();
            }
            var intCnt = list.Count;
            return Json(new { total = intCnt, data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "取得员工详细信息")]
        public ActionResult EmployeeDetail(string empId)
        {
            u_employee emp = new BLEmployee().GetEmployeeById(empId);
            if (emp == null) { emp = new u_employee(); }
            return View(emp);
        }

        [HttpPost]
        [Display(Description = "保存员工详细信息")]
        public ActionResult EmployeeSave(u_employee emp) {
            if (emp == null) { return Json("未取到比要参数", JsonRequestBehavior.AllowGet); }
            ActionType action = ActionType.Edit;
            BLEmployee bl = new BLEmployee();
            string user = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            if (string.IsNullOrEmpty(emp.ue_id))
            {
                action = ActionType.AddNew;
                emp.ue_id = bl.GetNextEmployeeId();
                emp.ue_create_time = DateTime.Now;
                emp.ue_create_user = user;
                emp.ue_status = "10";//在岗
            }
            else if (emp.ue_exit_time.HasValue && emp.ue_exit_time.Value.CompareTo(DateTime.Today) < 0) {
                emp.ue_status = "20";//离职
            }
            emp.ue_update_time= DateTime.Now;
            emp.ue_update_user = user;
            List<u_employee> list = new List<u_employee>();
            list.Add(emp);
            int intResult= bl.SaveEmployee(list,action);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "删除员工详细信息")]
        public ActionResult EmployeeDel(string empid) {
            if(string.IsNullOrEmpty(empid)) return Json("未取到比要参数", JsonRequestBehavior.AllowGet);
            BLEmployee bl = new BLEmployee();
            u_employee emp= bl.GetEmployeeById(empid);
            if (emp == null) { return Json("员工信息不存在，请重新检索", JsonRequestBehavior.AllowGet); }
            if (emp.ue_entry_time.HasValue && emp.ue_entry_time.Value.CompareTo(DateTime.Today) <= 0) {
               return Json("员工已入职，不允许删除，请走离职程序！", JsonRequestBehavior.AllowGet);
            }
            string user = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            emp.ue_status = "99";//删除
            emp.ue_update_time = DateTime.Now;
            emp.ue_update_user = user;
            List<u_employee> list = new List<u_employee>();
            list.Add(emp);
            int intResult = bl.SaveEmployee(list, ActionType.Edit);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
    }
}