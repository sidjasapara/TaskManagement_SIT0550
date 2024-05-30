using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Helper.Helpers;

namespace WebApplication1.CustomFilters
{
    public class StudentRoleFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool student = false;
            if (SessionHelper.Role == "Student" && SessionHelper.UserName != "")
            {
                student = true;
            }
            return student;
        }
    }
}