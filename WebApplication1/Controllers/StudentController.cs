using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Common;
using WebApplication1.CustomFilters;
using WebApplication1.Model.Models;
using WebApplication1.Repository.Repositories;
using WebApplication1.Repository.Services;

namespace WebApplication1.Controllers
{
    [CustomAuthorizeFilter]
    [CustomExceptionFilter]
    [StudentRoleFilter]
    public class StudentController : Controller
    {
        private readonly ITasksInterface _TasksService;
        private readonly IStudentsInterface _StudentsService;
        private readonly IAssignmentInterface _AssignmentService;
        public StudentController()
        {
            _TasksService = new TasksService();
            _StudentsService = new StudentsService();
            _AssignmentService = new AssignmentService();
        }

        public async Task<ActionResult> Dashboard()
        {
            int id = (int)Session["Id"];
            //ViewBag.total = _AssignmentService.TotalAssignments(id);
            //ViewBag.pending = _AssignmentService.PendingAssignments(id);
            //ViewBag.submitted = _AssignmentService.SubmittedAssignments(id);
            string url1 = $"api/StudentApi/TotalAssignments?id=" + id;
            string response1 = await WebApiHelper.HttpClientRequestResponse(url1);
            string url2 = $"api/StudentApi/PendingAssignments?id=" + id;
            string response2 = await WebApiHelper.HttpClientRequestResponse(url2);
            string url3 = $"api/StudentApi/SubmittedAssignments?id=" + id;
            string response3 = await WebApiHelper.HttpClientRequestResponse(url3);

            ViewBag.total = JsonConvert.DeserializeObject<int>(response1);
            ViewBag.pending = JsonConvert.DeserializeObject<int>(response2);
            ViewBag.submitted = JsonConvert.DeserializeObject<int>(response3);
            ViewBag.past = ViewBag.total - ViewBag.pending - ViewBag.submitted;
            return View();
        }

        // GET: Student
        public async Task<ActionResult> Index()
        {
            try
            {
                int id = (int)Session["Id"];
                //List<AssignmentModel> assignmentModels = _AssignmentService.MyAssignments(id);
                string url1 = $"api/StudentApi/MyAssignments?id=" + id;
                string response1 = await WebApiHelper.HttpClientRequestResponse(url1);
                List<AssignmentModel> assignmentModels = JsonConvert.DeserializeObject<List<AssignmentModel>>(response1);

                List <List<string>> tasksAssigned = new List<List<string>>();

                foreach (AssignmentModel a in assignmentModels)
                {
                    string assignId = Convert.ToString(a.assignmentId);
                    string assignStatus = Convert.ToString(a.status);
                    //List<string> t = _StudentsService.GetTaskName(a.taskId);

                    string url2 = $"api/StudentApi/GetTaskName?taskId=" + a.taskId;
                    string response2 = await WebApiHelper.HttpClientRequestResponse(url2);
                    List<string> t = JsonConvert.DeserializeObject<List<string>>(response2);
                    t.Add(assignId);
                    t.Add(assignStatus);
                    tasksAssigned.Add(t);
                }

                ViewBag.Tasks = tasksAssigned;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        public async Task<ActionResult> Pending()
        {
            try
            {
                int id = (int)Session["Id"];
                //List<AssignmentModel> assignmentModels = _AssignmentService.MyAssignments(id);
                string url1 = $"api/StudentApi/MyAssignments?id=" + id;
                string response1 = await WebApiHelper.HttpClientRequestResponse(url1);
                List<AssignmentModel> assignmentModels = JsonConvert.DeserializeObject<List<AssignmentModel>>(response1);

                List<List<string>> tasksAssigned = new List<List<string>>();

                foreach (AssignmentModel a in assignmentModels)
                {
                    if (a.status == 0)
                    {
                        string assignId = Convert.ToString(a.assignmentId);
                        string assignStatus = Convert.ToString(a.status);
                        //List<string> t = _StudentsService.GetTaskName(a.taskId);
                        string url2 = $"api/StudentApi/GetTaskName?taskId=" + a.taskId;
                        string response2 = await WebApiHelper.HttpClientRequestResponse(url2);
                        List<string> t = JsonConvert.DeserializeObject<List<string>>(response2);

                        t.Add(assignId);
                        t.Add(assignStatus);
                        DateTime dt = Convert.ToDateTime(t[2]);
                        if (dt > DateTime.Now)
                        {
                            tasksAssigned.Add(t);
                        }
                    }
                }

                ViewBag.Tasks = tasksAssigned;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        public async Task<ActionResult> Submitted()
        {
            try
            {
                int id = (int)Session["Id"];
                //List<AssignmentModel> assignmentModels = _AssignmentService.MyAssignments(id);
                string url1 = $"api/StudentApi/MyAssignments?id=" + id;
                string response1 = await WebApiHelper.HttpClientRequestResponse(url1);
                List<AssignmentModel> assignmentModels = JsonConvert.DeserializeObject<List<AssignmentModel>>(response1);

                List<List<string>> tasksAssigned = new List<List<string>>();

                foreach (AssignmentModel a in assignmentModels)
                {
                    if (a.status == 1)
                    {
                        string assignId = Convert.ToString(a.assignmentId);
                        string assignStatus = Convert.ToString(a.status);
                        //List<string> t = _StudentsService.GetTaskName(a.taskId);
                        string url2 = $"api/StudentApi/GetTaskName?taskId=" + a.taskId;
                        string response2 = await WebApiHelper.HttpClientRequestResponse(url2);
                        List<string> t = JsonConvert.DeserializeObject<List<string>>(response2);
                        t.Add(assignId);
                        t.Add(assignStatus);
                        tasksAssigned.Add(t);
                    }
                }

                ViewBag.Tasks = tasksAssigned;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        public async Task<ActionResult> Delayed()
        {
            try
            {
                int id = (int)Session["Id"];
                //List<AssignmentModel> assignmentModels = _AssignmentService.MyAssignments(id);
                string url1 = $"api/StudentApi/MyAssignments?id=" + id;
                string response1 = await WebApiHelper.HttpClientRequestResponse(url1);
                List<AssignmentModel> assignmentModels = JsonConvert.DeserializeObject<List<AssignmentModel>>(response1);

                List<List<string>> tasksAssigned = new List<List<string>>();

                foreach (AssignmentModel a in assignmentModels)
                {
                    if (a.status == 0)
                    {
                        string assignId = Convert.ToString(a.assignmentId);
                        string assignStatus = Convert.ToString(a.status);
                        //List<string> t = _StudentsService.GetTaskName(a.taskId);
                        string url2 = $"api/StudentApi/GetTaskName?taskId=" + a.taskId;
                        string response2 = await WebApiHelper.HttpClientRequestResponse(url2);
                        List<string> t = JsonConvert.DeserializeObject<List<string>>(response2);
                        t.Add(assignId);
                        t.Add(assignStatus);
                        DateTime dt = Convert.ToDateTime(t[2]);
                        if (dt < DateTime.Now)
                        {
                            tasksAssigned.Add(t);
                        }
                    }
                }

                ViewBag.Tasks = tasksAssigned;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        public void UpdateStatus(int assignmentId)
        {
            try
            {
                _AssignmentService.UpdateAssignmentStatus(assignmentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}