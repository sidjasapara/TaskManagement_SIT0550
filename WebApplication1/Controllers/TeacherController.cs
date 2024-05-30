using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Model.Models;
using WebApplication1.Repository.Repositories;
using WebApplication1.Repository.Services;
using WebApplication1.CustomFilters;
using System.Threading.Tasks;
using WebApplication1.Common;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [CustomAuthorizeFilter]
    [CustomExceptionFilter]
    [RoleFilter]
    public class TeacherController : Controller
    {
        private readonly ITasksInterface _TasksService;
        private readonly IStudentsInterface _StudentsService;
        private readonly IAssignmentInterface _AssignmentService;
        public TeacherController()
        {
            _TasksService = new TasksService();
            _StudentsService = new StudentsService();
            _AssignmentService = new AssignmentService();
        }

        public ActionResult DashBoard()
        {
            int id = (int)Session["Id"];
            ViewBag.created = _TasksService.TasksCreated(id);
            ViewBag.assigned = _AssignmentService.CountAssignedBy(id);
            ViewBag.pending = _AssignmentService.PendingAssigned(id);
            ViewBag.completed = _AssignmentService.CompletedAssigned(id);
            ViewBag.ntasks = _AssignmentService.TasksAssigned(id);
            return View();
        }

        // GET: Teacher
        public async Task<ActionResult> Index(int? current)
        {
            try
            {
                int id = (int)Session["Id"];

                string url = $"api/TeacherApi/GetTasksList?id={id}";
                string response = await WebApiHelper.HttpClientRequestResponse(url);

                //List<TaskModel> taskModels = _TasksService.GetTasksList(id);
                List<TaskModel> taskModels = JsonConvert.DeserializeObject<List<TaskModel>>(response);

                int maxRows = 3;
                int currentIndex = current ?? 1;
                List<TaskModel> list = taskModels.Skip((currentIndex - 1) * maxRows).Take(maxRows).ToList();
                PaginationModel paginationModel = new PaginationModel();
                paginationModel.currentIndex = currentIndex;
                paginationModel.taskModels = list;
                paginationModel.total = taskModels.Count();

                ViewBag.count = paginationModel.total;
                ViewBag.currentIndex = paginationModel.currentIndex;

                ViewBag.taskModel = paginationModel.taskModels;

                return View(new List<PaginationModel> { paginationModel });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateTask(TaskModel taskModel)
        {
            try
            {
                if (ModelState.IsValid && taskModel != null)
                {
                    //_TasksService.AddTask(taskModel);
                    string jsonContent = JsonConvert.SerializeObject(taskModel);
                    string url = $"api/TeacherApi/AddTask";
                    string response = await WebApiHelper.HttpClientPostRequest(url, jsonContent);
                    TaskModel task = JsonConvert.DeserializeObject<TaskModel>(response);
                    ViewBag.TaskAdded = "New Task Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }

        public async Task<ActionResult> EditTask(int id)
        {
            //TaskModel taskModel = _TasksService.GetTask(id);
            string url = $"api/TeacherApi/GetTask?id=" + id;
            string response = await WebApiHelper.HttpClientRequestResponse(url);
            TaskModel taskModel = JsonConvert.DeserializeObject<TaskModel>(response);
            return View(taskModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditTask(TaskModel taskModel)
        {
            try
            {
                if (taskModel != null && ModelState.IsValid)
                {
                    //_TasksService.EditTask(taskModel);
                    string jsonContent = JsonConvert.SerializeObject(taskModel);
                    string url = $"api/TeacherApi/EditTask";
                    string response = await WebApiHelper.HttpClientPostRequest(url, jsonContent);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            //List<TasksAssignedModel> list = _TasksService.GetDetails(id);
            string url = $"api/TeacherApi/GetDetails?id=" + id;
            string response = await WebApiHelper.HttpClientRequestResponse(url);
            List<TasksAssignedModel> list = JsonConvert.DeserializeObject<List<TasksAssignedModel>>(response);
            return View(list);
        }

        public async Task<ActionResult> AssignTask(int id)
        {
            //TaskModel taskModel = _TasksService.GetTask(id);
            string url1 = $"api/TeacherApi/GetTask?id=" + id;
            string response1 = await WebApiHelper.HttpClientRequestResponse(url1);

            TaskModel taskModel = JsonConvert.DeserializeObject<TaskModel>(response1);

            if (taskModel.deadline < DateTime.Now)
            {
                return PartialView("Error");
            }
            else
            {
                ViewBag.taskId = id;
                //ViewBag.studentList = _StudentsService.GetStudents(id);
                string url2 = $"api/TeacherApi/GetStudents?taskId=" + id;
                string response2 = await WebApiHelper.HttpClientRequestResponse(url2);
                ViewBag.studentList = JsonConvert.DeserializeObject<List<StudentModel>>(response2);
                ViewBag.students = new MultiSelectList(JsonConvert.DeserializeObject<List<StudentModel>>(response2), "studentId", "username");
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> AssignTask(int id, List<int> studentId)
        {
            try
            {
                ViewBag.taskId = id;
                //ViewBag.studentList = _StudentsService.GetStudents(id);
                //ViewBag.students = new MultiSelectList(_StudentsService.GetStudents(id), "studentId", "username");
                //string url = $"api/TeacherApi/GetStudents?taskId=" + id;
                //string response = await WebApiHelper.HttpClientRequestResponse(url);

                //ViewBag.students = new MultiSelectList(JsonConvert.DeserializeObject<List<StudentModel>>(response), "studentId", "username");

                //bool added = _AssignmentService.NewAssignment(id, studentId);

                string jsonContent = JsonConvert.SerializeObject(studentId);
                string url = $"api/TeacherApi/NewAssignment?taskId=" + id;
                string response = await WebApiHelper.HttpClientPostRequest(url, jsonContent);

                if (Convert.ToBoolean(response))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}