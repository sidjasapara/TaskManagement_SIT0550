using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.DBContext;
using WebApplication1.Model.Models;

namespace WebApplication1.Helper.Helpers
{
    public class UsersHelper
    {
        public static users CustomToDB(UserModel userModel)
        {
            try
            {
                users user = new users();
                if (userModel != null)
                {
                    user.role = userModel.role;
                    user.username = userModel.username;
                    user.email = userModel.email;
                    user.password = userModel.password;
                    user.contact = userModel.contact;
                    user.address = userModel.address;
                    user.stateId = userModel.stateId;
                    user.cityId = userModel.cityId;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static teachers CustomToDB(TeacherModel userModel)
        {
            try
            {
                teachers user = new teachers();
                if (userModel != null)
                {
                    user.username = userModel.username;
                    user.email = userModel.email;
                    user.password = userModel.password;
                    user.contact = userModel.contact;
                    user.address = userModel.address;
                    user.stateId = userModel.stateId;
                    user.cityId = userModel.cityId;
                    user.teacherId = userModel.teacherId;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static students CustomToDB(StudentModel userModel)
        {
            try
            {
                students user = new students();
                if (userModel != null)
                {
                    user.username = userModel.username;
                    user.email = userModel.email;
                    user.password = userModel.password;
                    user.contact = userModel.contact;
                    user.address = userModel.address;
                    user.stateId = userModel.stateId;
                    user.cityId = userModel.cityId;
                    user.studentId = userModel.studentId;
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
