using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum UserOperationalLogAction
    {
        AddNew = 0,
        Edit = 1,
        Delete = 2,
        Save = 3,
        Print = 4,
        Search = 5,
        Login = 6,
        Logout = 7,
        Upload = 8,
        Download = 9,
        Import = 10,
        Export = 11,
        Confirm = 12
    }

    public enum ActionType
    {
        AddNew = 0,
        Edit = 1,
        Delete = 2,      
        Confirm = 3,
        Reject =4
    }
    /// <summary>
    /// 设置菜单权限时的数据来源
    /// </summary>
    public enum MenuDataType {
        Group=0,
        User=1
    }
    /// <summary>
    /// 组织类型
    /// </summary>
    public enum OrganizationType {
        Company=0,
        Department=1,
        PositionLevel=2
    }
}
