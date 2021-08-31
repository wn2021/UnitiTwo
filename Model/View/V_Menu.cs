using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 菜单下拉框
    /// </summary>
    [Serializable()]
    public class Menu_B
    {
        public string id { get; set; }
        public string text { get; set; }
    }
    [Serializable()]
    public class V_Menu_List {
        public string m_id { get; set; }
        public int m_level { get; set; }
        public string m_level_name { get; set; }
        public string m_parent_id { get; set; }
        public string m_parent_text { get; set; }
        public string m_iconCls { get; set; }
        public string m_text { get; set; }
        public string m_url { get; set; }
        public string m_status { get; set; }
        public string m_status_name { get; set; }
        public DateTime? m_create_time { get; set; }
        public string m_create_user { get; set; }
        public DateTime? m_update_time { get; set; }
        public string m_update_user { get; set; }
        public string auth_flag { get; set; }
        public List<V_Menu_List> children { get; set; }
    }
    [Serializable()]
    public class V_Menu:ModelBase
    {
        public string id { get; set; }
        public string iconCls { get; set; }
        public string text { get; set; }
        public string url { get; set; }
    }
    [Serializable()]
    public class V_MenuOne : ModelBase
    {
        //菜单编号
        public string id { get; set; }
        //calsss
        public string iconCls { get; set; }
        //菜单文字
        public string text { get; set; }
        //菜单url
        public string url { get; set; }
        //子菜单
        public List<V_Menu> children { get; set; }
    }
    [Serializable()]
    public class V_Menu1Zero : ModelBase
    {
        //菜单编号
        public string id { get; set; }
        //calsss
        public string iconCls { get; set; }
        //菜单文字
        public string text { get; set; }
        //菜单url
        public string url { get; set; }
        //子菜单
        public List<V_MenuOne> children { get; set; }
    }
    [Serializable()]
    public class V_Menu_Tree {
        public string id { get; set; }
        public string text { get; set; }
        public string parentid { get; set; }

        public bool expanded { get; set; }
    }
}
