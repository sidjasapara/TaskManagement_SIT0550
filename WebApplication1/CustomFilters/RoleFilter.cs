using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Helper.Helpers;

namespace WebApplication1
{
    public class RoleFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool teacher = false;
            if (SessionHelper.Role == "Teacher" && SessionHelper.UserName != "")
            {
                teacher = true;
            }
            return teacher;
        }
    }
}