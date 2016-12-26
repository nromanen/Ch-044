using Model.DB;
using Model.DTO;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IUserManager
    {
        void UpdateUser(int Id, string UserName, string Password, string Email, int RoleId);
        UserDTO GetById(int id);
        UserDTO GetByEmail(string email, string password);
        UserDTO GetByUserName(string userName, string password);
        NetworkUserDTO GetNetworkUser(string networkAccountId, string network);

        List<UserDTO> GetAll();

        void Insert(UserDTO user);
        void Insert(NetworkUserDTO user);
		string GetEmail(int Id);
		bool EmailIsExist(string email);
        bool NetworkAccountExict(string networkAccountId, string network);
		List<UserDTO> GetUsersSize(int skip, int pageSize);
		int GetUsersSize();

	}
}