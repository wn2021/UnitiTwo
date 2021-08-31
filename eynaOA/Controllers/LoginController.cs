using Common;
using eynaOA.Results;
using Model.OA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VueWeb.Helper;

namespace eynaOA.Controllers
{
    public class LoginController : BaseController
    {
        /// <summary>        
        /// /// 登录        /// </summary>        /// <param name="account"></param>        /// <param name="password"></param>        /// <returns></returns>        
        [HttpGet]
        public Results.BaseDataPackage<User> check()
        {
            var result = new Results.BaseDataPackage<User>();
            string strResult = "";
            IDictionary<string, string> parameters = this.GetParameters();
            string[] KEYDATA = parameters["KEYDATA"].Replace("qq313596790fh", "").Split(';');
            if (KEYDATA != null && KEYDATA.Length == 2)
            {
                string username = KEYDATA[0];
                string password = KEYDATA[1];
                password = SHAEncry.StringEncryption(password + username);
                if (username != "admin"
                    || password != "de41b7fb99201d8334c23c014db35ecd92df81bc")
                {
                    strResult = "usererror";
                }
                else {
                    User user = new User();
                    user.USER_ID = "1";
                    user.USERNAME = "admin";
                    user.PASSWORD = "1";
                    user.ROLE_ID = "1";
                    user.STATUS = "0";
                    user.IP = "127.0.0.1";
                    user.NAME = "系统管理员";
                    user.LAST_LOGIN = "2021-01-08 13:46:39";
                    result.data = user;
                    strResult = "success";
                }
            }
            else {
                strResult = "error";
            }
            result.result = strResult;
            return result;
        }
    }
}
