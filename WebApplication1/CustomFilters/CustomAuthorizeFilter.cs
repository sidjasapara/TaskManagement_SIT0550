using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Helper.Helpers;

namespace WebApplication1
{
    public class CustomAuthorizeFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorizedUser = false;
            if (SessionHelper.Role != "" && SessionHelper.UserName != "")
            {
                authorizedUser = true;
            }
            return authorizedUser;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "LogIn" },
                    { "action", "LogIn" }
               });
        }
    }
}