using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Helper.Helpers
{
    public class SessionHelper
    {
        public static int Id
        {
            get
            {
                return HttpContext.Current.Session["Id"] == null ? 0 : (int)HttpContext.Current.Session["Id"];
            }
            set
            {
                HttpContext.Current.Session["Id"] = value;
            }
        }
        public static string Role
        {
            get
            {
                return HttpContext.Current.Session["Role"] == null ? "" : (string)HttpContext.Current.Session["Role"];
            }
            set
            {
                HttpContext.Current.Session["Role"] = value;
            }
        }
        public static string UserName
        {
            get
            {
                return HttpContext.Current.Session["UserName"] == null ? "" : (string)HttpContext.Current.Session["UserName"];
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }
    }
}
