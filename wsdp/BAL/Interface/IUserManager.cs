using Model.DTO;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IUserManager
    {
        void UpdateUser(int Id, string UserName, string Password, string Email, int RoleId);

        UserDTO GetUser(string email, string password);
        UserDTO GetSocialNetworkUser(string uId, string network);

        List<UserDTO> GetAll();

        void Insert(UserDTO user);

        bool EmailIsExist(string email);
    }
}