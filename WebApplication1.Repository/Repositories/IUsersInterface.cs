using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.Models;

namespace WebApplication1.Repository.Repositories
{
    public interface IUsersInterface
    {
        bool CheckIfExist(UserModel userModel);
        UserModel AddUser(UserModel userModel);
        bool MatchCredentials(UserLoginModel userLoginModel);
    }
}
