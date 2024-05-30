using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApplication1.Common;
using WebApplication1.CustomFilters;
using WebApplication1.Helper.Helpers;
using WebApplication1.Model.DBContext;
using WebApplication1.Model.Models;
using WebApplication1.Repository.Repositories;
using WebApplication1.Repository.Services;

namespace TaskManagement_SIT0550.Controllers
{
    [CustomExceptionFilter]
    public class LogInController : Controller
    {
        private TaskManagementEntities DBContext = new TaskManagementEntities();

        private readonly IUsersInterface _UsersService;

        public LogInController()
        {
            _UsersService = new UsersService();
        }

        // GET: LogIn
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserLoginModel userLoginModel)
        {
            try
            {
                if (ModelState.IsValid && userLoginModel != null)
                {
                    bool login = _UsersService.MatchCredentials(userLoginModel);

                    //string jsonContent = JsonConvert.SerializeObject(userLoginModel);
                    //string url = $"api/LoginApi/MatchCredentials";
                    //string response = await WebApiHelper.HttpClientPostRequest(url, jsonContent);

                    //bool login = JsonConvert.DeserializeObject<bool>(response);

                    if (login)
                    {
                        TempData["Message"] = "Login Successful";
                        if ((string)Session["Role"] == "Teacher")
                        {
                            TempData["LoggedIn"] = "Logged In";
                            return RedirectToAction("Dashboard", "Teacher");
                        }
                        else
                        {
                            TempData["LoggedIn"] = "Logged In";
                            return RedirectToAction("Dashboard", "Student");
                        }
                    }
                    else
                    {
                        ViewBag.LoginFailed = "Invalid credentials";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid && userModel != null)
                {
                    //bool existingUser = _UsersService.CheckIfExist(userModel);
                    string jsonContent = JsonConvert.SerializeObject(userModel);
                    string url = $"api/LoginApi/CheckIfExist";
                    string response = await WebApiHelper.HttpClientPostRequest(url, jsonContent);

                    bool existingUser = JsonConvert.DeserializeObject<bool>(response);
                    if (existingUser)
                    {
                        ViewBag.ExistingUser = "Email already Registered";
                        return View();
                    }
                    else
                    {
                        //_UsersService.AddUser(userModel);
                        string jsonContent2 = JsonConvert.SerializeObject(userModel);
                        string url2 = $"api/LoginApi/AddUser";
                        string response2 = await WebApiHelper.HttpClientPostRequest(url2, jsonContent2);

                        bool added = JsonConvert.DeserializeObject<bool>(response2);
                        TempData["Registered"] = "User Registerd";
                        return RedirectToAction("LogIn", "LogIn");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
        public JsonResult GetCities(int Id)
        {
            var city = DBContext.cities.Where(c => c.stateId == Id).ToList();
            var result = city.Select(c => new { value = c.cityId, text = c.cityName });
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["LoggedOut"] = "Session Ended";
            return RedirectToAction("LogIn", "LogIn");
        }
    }
}