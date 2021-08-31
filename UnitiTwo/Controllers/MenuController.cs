using Business;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace UnitiTwo.Controllers
{
    [Authentication]
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        #region 菜单列表画面
        [HttpGet]
        public ActionResult MenuList() {
            return View();
        }

        [HttpPost]
        [Display(Description = "取得菜单列表")]
        public ActionResult GetMenuList(int? pageIndex, int? pageSize,string key,string memulevel, string pid) {
            List<V_Menu_List> lst = new List<V_Menu_List>();
            BLMenu bl = new BLMenu();
            pageIndex = pageIndex ?? 0;
            pageSize = pageSize ?? 20;
            var level = -1;
            if (!string.IsNullOrEmpty(memulevel))
            {
                //菜单层级不为空
                level = int.Parse(memulevel);
                lst = bl.GetMenus(level, pid);                
            }
            else {
                lst = bl.GetMenus();
                if (lst == null) { lst = new List<V_Menu_List>(); }
                //父菜单
                if (!string.IsNullOrEmpty(pid) && lst.Count>0)
                {
                    lst = lst.FindAll(m => m.m_parent_id == pid);
                }
            }
            if (lst == null) { lst= new List<V_Menu_List>(); }
            //根据文本查询
            if (!string.IsNullOrEmpty(key) && lst.Count>0)
            {
                lst = lst.FindAll(m => m.m_text.Contains(key));
            }
            var total = lst.Count;
            var list = lst.OrderBy(d => d.m_id).Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "删除菜单")]
        public ActionResult MenuDel(string menuId)
        {
            if (string.IsNullOrEmpty(menuId)) { return Json("未取得菜单编号。"); }
            string[] arr = menuId.Split(',');
            BLMenu bl = new BLMenu();
            List<menu> list = new List<menu>();
            menu me = null;
            string delUser = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            DateTime delTime = DateTime.Now;
            for (int i = 0; i < arr.Length; i++)
            {               
                me = bl.GetMenuById(arr[i]);
                if (me == null) { continue; }
                List<V_Menu_List> child = bl.GetMenus(me.m_level+1,me.m_id);
                if (child != null && child.Count > 0) {
                    if (child.FindAll(c => c.m_status != "9").Count > 0) {
                        return Json(string.Format("菜单[{0}]下面有有效子菜单，不允许删除",me.m_text));
                    }
                }
                me.m_status = "9";
                me.m_update_user = delUser;
                me.m_update_time = delTime;
                list.Add(me);
            }
            int intResult = bl.UpdateMenu(list);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "删除菜单")]
        public ActionResult MenuRecover(string menuId)
        {
            if (string.IsNullOrEmpty(menuId)) { return Json("未取得菜单编号。"); }
            string[] arr = menuId.Split(',');
            BLMenu bl = new BLMenu();
            List<menu> list = new List<menu>();
            menu me = null;
            string delUser = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            DateTime delTime = DateTime.Now;
            for (int i = 0; i < arr.Length; i++)
            {
                me = bl.GetMenuById(arr[i]);
                if (me == null) { continue; }
                me.m_status = "1";
                me.m_update_user = delUser;
                me.m_update_time = delTime;
                list.Add(me);
            }
            int intResult = bl.UpdateMenu(list);
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        #endregion 菜单列表画面

        #region 共通
        [HttpGet]
        [Display(Description = "加载菜单基本信息")]
        public ActionResult MenuBasic(string childLevel) {
            List<Menu_B> cmbList = new List<Menu_B>();
            
            if (string.IsNullOrEmpty(childLevel)) {
                return Json(new { data = cmbList }, JsonRequestBehavior.AllowGet);
            }
            int level = int.Parse(childLevel) - 1;
            BLMenu bl = new BLMenu();
            List<V_Menu_List> lst = bl.GetMenus(level,"");
            
            if (lst != null && lst.Count > 0) {
                Menu_B mb = null;
                foreach (V_Menu_List me in lst) {
                    if (me.m_status != "1") { continue; }
                    mb = new Menu_B();
                    mb.id = me.m_id;
                    mb.text = me.m_text;
                    cmbList.Add(mb);
                }
            }
            return Json(new { data = cmbList }, JsonRequestBehavior.AllowGet);
        }
        #endregion 共通

        #region 菜单编辑画面
        [HttpGet]
        [Display(Description = "加载权限组信息")]
        public ActionResult MenuEdit(string id)
        {
            menu n;
            if (string.IsNullOrEmpty(id))
                n = new menu();
            else
            {
                n = new BLMenu().GetMenuById(id);
                if (n == null)
                    return View("无权限信息");               
            }

            return View(n);
        }
        [HttpPost]
        [Display(Description = "新增&编辑菜单")]
        public ActionResult MenuSave(menu me)
        {
            if (me == null) { return Json("未取得需要保存的信息"); }
            ActionType at = ActionType.Edit;
            BLMenu bl = new BLMenu();
            string saveUser = (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
            if (string.IsNullOrEmpty(me.m_id))
            {
                me.m_id = bl.GetNewMenuId(me.m_level, me.m_parent_id);
                me.m_status = "1";
                me.m_create_time = DateTime.Now;
                me.m_create_user = saveUser;
                at = ActionType.AddNew;
            }
            me.m_update_user = saveUser;
            me.m_update_time = DateTime.Now;
            List<menu> list = new List<menu>();
            list.Add(me);
            int intResult = -1;
            if (at == ActionType.AddNew)
            {
                intResult = bl.InsertMenu(list);
            }
            else {
                intResult = bl.UpdateMenu(list);
            }
            return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
        }
        #endregion 菜单编辑画面

        #region 菜单选择画面
        [HttpGet]
        [Display(Description = "加载菜单权限画面")]
        public ActionResult MenuSelect(string id,int dataType) {
            V_Menu_Select ms = new V_Menu_Select();
            ms.key_id = id;
            ms.key_type = dataType;
            return View(ms);
        }
        [HttpGet]
        [Display(Description = "加载菜单权限画面")]
        public ActionResult SelectMenu(string id, int dataType)
        {
            V_Menu_Select ms = new V_Menu_Select();
            ms.key_id = id;
            ms.key_type = dataType;
            return View(ms);
        }
        [HttpPost]
        [Display(Description = "根据用户或权限组获取可选菜单")]
        public ActionResult GetMenuForSelect(int? pageIndex, int? pageSize, string id, string dataType,string memulevel, string pid) {
            int intLevel = -1;
            BLMenu bl = new BLMenu();
            List<V_Menu_List> menus = new List<V_Menu_List>();
            if (!string.IsNullOrEmpty(memulevel)) {
                intLevel = int.Parse(memulevel);
            }
            if (dataType == "0") {            
                menus = bl.GetGroupMenus(id, intLevel, pid);
            }else if (dataType == "1")
            {
                menus = bl.GetUserMenus(id, intLevel, pid);
            }
            var total = menus.Count;
            var list = menus.Skip((pageIndex * pageSize).Value)
         .Take((pageSize).Value).ToList();
            return Json(new { total = total, data = menus }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Display(Description = "根据用户或权限组获取可选菜单")]
        public ActionResult GetSelectMenu(string id, string dataType, string memulevel, string pid) {
            int intLevel = -1;
            BLMenu bl = new BLMenu();
            List<V_Menu_List> menus = new List<V_Menu_List>();
            if (!string.IsNullOrEmpty(memulevel))
            {
                intLevel = int.Parse(memulevel);
            }
            if (dataType == "0")
            {
                menus = bl.GetGroupMenus(id, intLevel, pid);
            }
            else if (dataType == "1")
            {
                menus = bl.GetUserMenus(id, intLevel, pid);
            }
            foreach (V_Menu_List me in menus) {
                if (string.IsNullOrEmpty(me.m_parent_id)) {
                    me.m_parent_id = "";
                }
            }
            return Json(new { data = menus }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Display(Description = "保存菜单权限")]
        public ActionResult MenuAuth(string menuIds,string key,string dataType,int level,string pid)
        {
            if (string.IsNullOrEmpty(menuIds) || string.IsNullOrEmpty(key) || string.IsNullOrEmpty(dataType)) {
                return Json("没有可保存的数据！");
            }
            string userid= (System.Web.HttpContext.Current.Session["username"] == null) ? "system" : System.Web.HttpContext.Current.Session["username"].ToString();
                        
            if (dataType == "0")
            {                
                List<group_menu_auth> list = new List<group_menu_auth>();
                string[] arr = menuIds.Split(',');
                group_menu_auth gm = null;
                for (int i = 0; i < arr.Length; i++)
                {
                    gm = new group_menu_auth();
                    gm.gm_group_id = key;
                    gm.gm_menu_id = arr[i];
                    gm.gm_status = "1";
                    gm.gm_update_time = DateTime.Now;
                    gm.gm_update_user = userid;
                    gm.gm_create_time = DateTime.Now;
                    gm.gm_create_user = userid;
                    list.Add(gm);
                }
                List<V_Menu_List> lst1 = new BLMenu().GetGroupMenus(key, level, pid);                
                List<group_menu_auth> list2 = new List<group_menu_auth>();
                if (lst1 != null && lst1.Count > 0)
                {
                    foreach (V_Menu_List ml in lst1)
                    {
                        if (ml.auth_flag == "0") { continue; }
                        if (list.FindAll(m => m.gm_menu_id == ml.m_id).Count == 0)
                        {
                            gm = new group_menu_auth();
                            gm.gm_group_id = key;
                            gm.gm_menu_id = ml.m_id;
                            gm.gm_status = "9";
                            gm.gm_update_time = DateTime.Now;
                            gm.gm_update_user = userid;
                            gm.gm_create_time = DateTime.Now;
                            gm.gm_create_user = userid;
                            list2.Add(gm);
                        }
                    }
                }
                if (list2 != null && list2.Count > 0)
                {
                    list.AddRange(list2);
                }
                int intResult =new BLMenu().SaveGroupMenus(list);
                return Json(new { @return = intResult }, JsonRequestBehavior.AllowGet);
            }
            else if (dataType == "1") {
                //用户菜单权限
                
            }
            return View();
        }
        #endregion 菜单选择画面

        #region 菜单树画面
        [HttpGet]
        public ActionResult MenuPage()
        {
            return View();
        }
        [HttpPost]
        [Display(Description = "取得菜单列表")]
        public ActionResult GetMenus()
        {
            BLMenu bl = new BLMenu();
            List<V_Menu_List> menus = bl.GetMenus();
            List<V_Menu_Tree> listResult = new List<V_Menu_Tree>();
            V_Menu_Tree mt = null;
            foreach (V_Menu_List me in menus)
            {
                mt = new V_Menu_Tree();
                mt.id = me.m_id;
                mt.text = me.m_text;
                mt.parentid = string.IsNullOrEmpty(me.m_parent_id) ? "" : me.m_parent_id;
                V_Menu_List child = menus.Find(m => m.m_parent_id == me.m_id);
                if (child != null)
                {
                    mt.expanded = true;
                }
                listResult.Add(mt);
            }
            //string treeJson = "";
            //StringBuilder comTreeResult = new StringBuilder();
            //foreach (V_Menu_Tree me in listResult)
            //{
            //    if (me.expanded)
            //    {
            //        comTreeResult.Append("{\"name\":\""+ me.text+"\", \"id\":\""+me.id+"\", expanded: true},");
            //    }
            //    else {
            //        comTreeResult.Append("{\"name\":\"" + me.text + "\", \"id\":\"" + me.id + "\", \"pid\": \""+me.parentid+"},");
            //    }
            //}
            //treeJson = comTreeResult.ToString();
            //if (!string.IsNullOrEmpty(treeJson)) {
            //    treeJson= "[" + treeJson.Remove(treeJson.Length - 1, 1) + "]";
            //}
            //return treeJson;

            return Json(new { data = listResult }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取得子菜单
        /// </summary>
        /// <param name="partenid"></param>
        /// <returns></returns>
        public ActionResult GetChildMenus(string partenid)
        {
            BLMenu bl = new BLMenu();
            // List<menu> menus = bl.GetMenus(-1, partenid);
            menu me = bl.GetMenuById(partenid);
            return Json(new { data = me }, JsonRequestBehavior.AllowGet);
        }
        #endregion 菜单树画面
    }    
}