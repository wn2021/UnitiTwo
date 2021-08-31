using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using VueWeb.Helper;

namespace VueWeb.Controller
{
    public class LoginController : ApiController
    {
        /// <summary>        
        /// /// 登录        /// </summary>        /// <param name="account"></param>        /// <param name="password"></param>        /// <returns></returns>        
        [HttpGet]
        public BaseDataPackage<UserData> Login(string account, string password)
        {
            //使用account和password验证用户            
            UserData userData = new UserData();
            userData.UserGuid = Guid.NewGuid().ToString();
            userData.UserName = "测试用户";

            //将登录信息形成票据并存入到cookie中            
            DateTime dateTime = DateTime.Now.AddMinutes(600);
            var ticketDataStr = Newtonsoft.Json.JsonConvert.SerializeObject(userData);
            var ticket = new FormsAuthenticationTicket(2, ticketDataStr, DateTime.Now, dateTime, false, "AspNetVueElementUI", "/");
            //建立身份验证票对象            
            string HashTicket = FormsAuthentication.Encrypt(ticket);
            //加密序列化验证票为字符串            
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
            //将cookie的失效时间设置为和票据tikets的失效时间一致             
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            //生成Cookie            
            HttpContext.Current.Response.Cookies.Add(cookie); //输出Cookie


            var result = new BaseDataPackage<UserData>();
            result.Data = userData;
            result.Status = ApiStatusCode.OK;
            result.Message = "登录成功";
            return result;
        }


        #region Logout
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public BaseDataPackage<string> Logout()
        {
            var result = new BaseDataPackage<string>();
            try
            {
                HttpCookie cookie = HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName];
                if (cookie == null)
                {
                    cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                HttpContext.Current.Response.Cookies.Clear();
                cookie.Expires = DateTime.Now.AddYears(-10);
                FormsAuthentication.SignOut();
                result.Status = ApiStatusCode.OK;
                result.Message = "登出成功";
                return result;
            }
            catch (Exception ex)
            {
                result.Status = ApiStatusCode.EXCEPTION;
                result.Message = "登出异常=>" + ex.Message;
                return result;
            }
        }

        #endregion


    }
}
