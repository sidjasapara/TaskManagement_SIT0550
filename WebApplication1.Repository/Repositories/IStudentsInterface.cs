using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.DBContext;
using WebApplication1.Model.Models;

namespace WebApplication1.Repository.Repositories
{
    public interface IStudentsInterface
    {
        List<students> GetStudents(int taskId);

        List<string> GetTaskName(int taskId);
    }
}
