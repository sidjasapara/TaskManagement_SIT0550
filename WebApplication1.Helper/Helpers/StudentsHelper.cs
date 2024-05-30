using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.DBContext;
using WebApplication1.Model.Models;

namespace WebApplication1.Helper.Helpers
{
    public class StudentsHelper
    {
        public static List<StudentModel> StudentsList(List<students> studentList)
        {
            try
            {
                List<StudentModel> studentModels = new List<StudentModel>();
                if (studentList != null && studentList.Count > 0)
                {
                    foreach (students s in studentList)
                    {
                        StudentModel student = new StudentModel();
                        student.studentId = s.studentId;
                        student.username = s.username;
                        student.email = s.email;
                        student.password = s.password;
                        student.contact = s.contact;
                        student.address = s.address;
                        student.cityId = (int)s.cityId;
                        student.stateId = (int)s.stateId;

                        studentModels.Add(student);
                    }
                }
                return studentModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
