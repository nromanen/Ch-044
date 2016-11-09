using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IUserManager
    {
        void UpdateUser(int Id, string UserName, string Password, string Email, int RoleId);
        UserDTO GetUser(string email, string password);

        List<UserDTO> GetAll();
        void Insert(UserDTO user);
        bool UserNameIsExist(string userName);
        bool EmailIsExist(string userName);
    }
}
