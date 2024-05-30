using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Helper.Helpers;
using WebApplication1.Model.DBContext;
using WebApplication1.Model.Models;
using WebApplication1.Repository.Repositories;


namespace WebApplication1.Repository.Services
{
    public class UsersService : IUsersInterface
    {
        private TaskManagementEntities DBContext = new TaskManagementEntities();

        public bool CheckIfExist(UserModel userModel)
        {
            try
            {
                bool userExist = true;

                users userModel1 = DBContext.users.FirstOrDefault(u => u.email == userModel.email);
                if (userModel1 == null)
                {
                    userExist = false;
                }

                return userExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel AddUser(UserModel userModel)
        {
            try
            {
                users user = UsersHelper.CustomToDB(userModel);
                DBContext.users.Add(user);
                DBContext.SaveChanges();

                if (userModel.role == "Teacher")
                {
                    TeacherModel teacher = new TeacherModel();
                    teacher.username = userModel.username;
                    teacher.email = userModel.email;
                    teacher.contact = userModel.contact;
                    teacher.password = userModel.password;
                    teacher.address = userModel.address;
                    teacher.cityId = userModel.cityId;
                    teacher.stateId = userModel.stateId;

                    teachers teacher1 = UsersHelper.CustomToDB(teacher);

                    DBContext.teachers.Add(teacher1);
                    DBContext.SaveChanges();
                }
                else if (userModel.role == "Student")
                {
                    StudentModel student = new StudentModel();
                    student.username = userModel.username;
                    student.email = userModel.email;
                    student.contact = userModel.contact;
                    student.password = userModel.password;
                    student.address = userModel.address;
                    student.cityId = userModel.cityId;
                    student.stateId = userModel.stateId;

                    students student1 = UsersHelper.CustomToDB(student);

                    DBContext.students.Add(student1);
                    DBContext.SaveChanges();
                }
                return userModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MatchCredentials(UserLoginModel userLoginModel)
        {
            try
            {
                bool login = false;
                users userModel1 = DBContext.users.FirstOrDefault(u => u.role == userLoginModel.role && u.email == userLoginModel.email && u.password == userLoginModel.password);
                if (userModel1 != null)
                {
                    if (userModel1.role == "Teacher")
                    {
                        teachers teacher = DBContext.teachers.FirstOrDefault(t => t.email == userLoginModel.email);
                        SessionHelper.Id = teacher.teacherId;
                    }
                    else
                    {
                        students student = DBContext.students.FirstOrDefault(s => s.email == userLoginModel.email);
                        SessionHelper.Id = student.studentId;
                    }
                    SessionHelper.Role = userModel1.role;
                    SessionHelper.UserName = userModel1.username;
                    login = true;
                }
                return login;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}