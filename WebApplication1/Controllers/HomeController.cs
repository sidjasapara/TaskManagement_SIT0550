using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1;
using WebApplication1.CustomFilters;

namespace TaskManagement_SIT0550.Controllers
{
    [CustomAuthorizeFilter]
    [CustomExceptionFilter]
    public class HomeController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }
    }
}