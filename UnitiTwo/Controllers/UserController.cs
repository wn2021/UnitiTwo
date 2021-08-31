using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Model;
using Business;
using Common;

using UnitiTwo.Models;

namespace UnitiTwo.Controllers
{
    
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            FormsAuthentication.SetAuthCookie("", false);//将用户名放入Cookie中
            System.Web.HttpContext.Current.Session.Clear();
            return View();
        }
        [HttpPost]
        [Display(Description = "用户登录验证")]
        public ActionResult Validation(V_UserInfo user)
        {
            if (user == null) {
                return Json("用户登录失败");
            }
            if (string.IsNullOrEmpty(user.uu_name)) { return Json("用户登录失败"); }
            if (string.IsNullOrEmpty(user.uu_password)) { return Json("用户登录失败"); }

            u_user info = new BLUserInfo().GetUserByKeyValue(user.uu_name);
            if (info == null) { return Json("用户不存在"); }
            if (info.UU_PASSWORD != user.uu_password) { return Json("密码错误"); }

            FormsAuthentication.SetAuthCookie(user.uu_name, false);//将用户名放入Cookie中
            System.Web.HttpContext.Current.Session["username"] = user.uu_name;
            System.Web.HttpContext.Current.Session["usernumber"] = info.UU_ID;

            return Json(new { @return = 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UserPage()
        {
            return View();
        }
        [Authentication]
        [HttpPost]
        [Display(Description = "取得用户列表")]
        public ActionResult UserList(int? pageIndex, int? pageSize)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
         //   List<u_user> lst = new BLUserInfo().GetUsers();
         //   var total = lst.Count;
         //   var list = lst.OrderByDescending(d => d.UU_ID).Skip((pageIndex * pageSize).Value)
         //.Take((pageSize).Value).Select(d => new
         //{
         //    d.UU_ID,
         //    d.UU_NAME,
         //    d.UU_EMAIL,
         //    d.UU_PHONE,
         //    d.UU_ADDRESS,
         //    d.UU_TYPE
         //}).ToList();
            List<V_UserInfo> lst = new BLUserInfo().GetUserList();
            var total = lst.Count;
            var list = lst.OrderBy(d => d.uu_id).Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [Authentication]
        [HttpGet]
        [Display(Description = "加载用户基本信息")]
        public ActionResult UserBasicEdit(int? id)
        {
            u_user n;
            if ((id ?? 0) == 0)
                n = new u_user();
            else
            {
                n = new BLUserInfo().GetUserById(id.ToString());
                if (n == null)
                    return View("无用户信息");
            }

            return View(n);
        }
        [HttpGet]
        [Display(Description = "加载用户基本信息")]
        public ActionResult UserInfo()
        {
            string userName= (System.Web.HttpContext.Current.Session["username"] == null) ? "" : System.Web.HttpContext.Current.Session["username"].ToString();
            u_user n;
            if (string.IsNullOrEmpty(userName))
                n = new u_user();
            else
            {
                n = new BLUserInfo().GetUserByKeyValue(userName);
                if (n == null)
                    n = new u_user();
            }

            return View(n);
        }
        [HttpPost]
        [Display(Description = "保存公司信息")]
        public ActionResult UserSave(u_user userInfo)
        {
            if (userInfo == null) { return View("无用户信息"); }
            if (userInfo.UU_ID == 0) {
                userInfo.UU_PASSWORD = "111111";
                userInfo.UU_STATUS = "10";
                userInfo.UU_CREATE_TIME = DateTime.Now;
                userInfo.UU_CREATE_USER = (System.Web.HttpContext.Current.Session["username"] == null) ? "": System.Web.HttpContext.Current.Session["username"].ToString();
            }
            userInfo.UU_UPDATE_USER = (System.Web.HttpContext.Current.Session["username"] == null) ? "" : System.Web.HttpContext.Current.Session["username"].ToString();
            userInfo.UU_UPDATE_TIME = DateTime.Now;
            new BLUserInfo().UserSave(userInfo);
            return Json(new { @return = 1 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
         public ActionResult UserDel(string userIds) {
            if (string.IsNullOrEmpty(userIds)) { return Json("参数为空"); }
            List<u_user> delList = new List<u_user>();           
            string[] arr = userIds.Split(',');
            u_user uu = null;
            for (int i = 0; i < arr.Length; i++) {
                uu = new u_user();
                uu.UU_ID = int.Parse(arr[0]);
                uu.UU_STATUS = "99";
                uu.UU_UPDATE_USER = (System.Web.HttpContext.Current.Session["username"] == null) ? "sys" : System.Web.HttpContext.Current.Session["username"].ToString();
                uu.UU_UPDATE_TIME = DateTime.Now;
                delList.Add(uu);
            }
            if (delList.Count == 0) { return Json("没有需要删除的用户。"); }
            string updateUser= (System.Web.HttpContext.Current.Session["username"] == null) ? "" : System.Web.HttpContext.Current.Session["username"].ToString();
            int intResult = new BLUserInfo().Delete(delList);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        [Display(Description = "修改当前用户密码")]
        public ActionResult ChangePsd(V_Password vp)
        {
            if (vp == null) {
                return Json("没有取得比要参数！");
            }
            if (string.IsNullOrEmpty(vp.old_password)) { return Json("没有取得必要参数！"); }
            if (string.IsNullOrEmpty(vp.new_password)) { return Json("没有取得必要参数！"); }
            if (string.IsNullOrEmpty(vp.cfrm_password)) { return Json("没有取得必要参数！"); }
            if (!vp.new_password.Equals(vp.cfrm_password)) { return Json("两次录入的新密码不一致！"); }
            string updateUser = (System.Web.HttpContext.Current.Session["username"] == null) ? "" : System.Web.HttpContext.Current.Session["username"].ToString();
            if (string.IsNullOrEmpty(updateUser)) {
                return Json("未取得当前用户信息！");
            }
            BLUserInfo bl = new BLUserInfo();
            u_user us = bl.GetUserByKeyValue(updateUser);
            if (us == null) { Json("未取得当前用户信息！"); }
            if (!us.UU_PASSWORD.Equals(vp.old_password)) { return Json("原密码错误！"); }
            us.UU_PASSWORD = vp.new_password;
            int intResult = bl.UserSave(us);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        #region 权限组信息
        [HttpGet]
        public ActionResult GroupPage()
        {
            return View();
        }
        [HttpGet]
        [Display(Description = "加载权限组信息")]
        public ActionResult GetGroup(string strStatus) {
            if (strStatus == null || strStatus == string.Empty) { strStatus = "10"; }
            List<mu_group> lst  = new BLGroup().GetGroupByStatus(strStatus);
            return Json(new { data = lst }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "加载权限组信息")]
        public ActionResult GroupList(int? pageIndex, int? pageSize,string key, string strStatus)
        {
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 10;
            List<mu_group> lst = new List<mu_group>();
            if (key != null && key.Trim() != "")
            {
                lst = new BLGroup().GetGroupByName(key);
            }
            else
            {               
                lst = new BLGroup().GetGroupByStatus(strStatus);
            }
            var total = lst.Count;
            var list = lst.OrderBy(d => d.mu_id).Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Display(Description = "加载权限组信息")]
        public ActionResult GroupEdit(int? id)
        {
            mu_group n;
            if ((id ?? 0) == 0)
                n = new mu_group();
            else
            {
                List< mu_group> lst = new BLGroup().GetGroupById(id.ToString());
                if (lst == null || lst.Count==0)
                    return View("无权限信息");
                n = lst[0];
            }

            return View(n);
        }
        [HttpPost]
        [Display(Description = "保存权限组信息")]
        public ActionResult GroupSave(mu_group grp)
        {
            if (grp == null) { return View("无权限组信息"); }
            ActionType at = ActionType.Edit;
            BLGroup bl = new BLGroup();
            if (grp.mu_id == 0)
            {
                grp.mu_id = bl.GetNextGroudId();
                grp.mu_status = "10";
                grp.mu_create_time = DateTime.Now;
                grp.mu_create_user = (System.Web.HttpContext.Current.Session["username"] == null) ? "" : System.Web.HttpContext.Current.Session["username"].ToString();
                at = ActionType.AddNew;
            }
            grp.mu_update_user = (System.Web.HttpContext.Current.Session["username"] == null) ? "" : System.Web.HttpContext.Current.Session["username"].ToString();
            grp.mu_update_time = DateTime.Now;
            int intResult = bl.GroupSave(grp,at);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "删除权限组信息")]
        public ActionResult GroupDel(string id) {
            if (string.IsNullOrEmpty(id)) { return Json("参数为空"); }
            BLGroup bl = new BLGroup();
            List<V_UserInfo> users = bl.GetReleatedUser(id);
            if (users != null && users.Count > 0) {
                return Json("存在有效用户属于要删除的权限组中，不允许删除！");
            }
            string updateUser= (System.Web.HttpContext.Current.Session["username"] == null) ? "" : System.Web.HttpContext.Current.Session["username"].ToString();
            int intResult = new BLGroup().Delete(id, updateUser);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        #endregion 权限组信息
    }
}