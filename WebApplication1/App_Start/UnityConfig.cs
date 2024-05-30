using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApplication1.Repository.Repositories;
using WebApplication1.Repository.Services;

namespace WebApplication1
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IUsersInterface, UsersService>();
            container.RegisterType<ITasksInterface, TasksService>();
            container.RegisterType<IStudentsInterface, StudentsService>();
            container.RegisterType<IAssignmentInterface, AssignmentService>();
        }
    }
}