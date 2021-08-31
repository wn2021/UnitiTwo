using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using VueWeb.App_Start;

namespace VueWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            // 提取窗体身份验证 cookie            
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];
            if (null == authCookie)            {
                // 没有身份验证 cookie。                
                return;
            }
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (null == authTicket)            {
                // 无法解密 Cookie。                
                return;
            }
            string[] roles = authTicket.UserData.Split(new char[] { ',' });
            FormsIdentity id = new FormsIdentity(authTicket);
            System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(id, roles);
            HttpContext.Current.User = principal;

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}