﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UnitiTwo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static bool isExpire = false; //Session是否超时
        protected void Session_End(object sender, EventArgs e)
        {
            isExpire = true;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            bool IsAjax = new HttpRequestWrapper(Request).IsAjaxRequest();
            if (isExpire && (IsAjax == false))
            {
                isExpire = false;
                Response.Write("<script languge='javascript'>alert('用户信息已过期，请重新登录');window.parent.location.href='/Home/Login';</script>");
                Response.End();
            }
        }

    }
}
