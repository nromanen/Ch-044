using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using log4net;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DB;

namespace BAL.Manager
{
    public class UserManager : BaseManager, IUserManager
    {
        public UserManager(IUnitOfWork uOW)
            : base(uOW)
        {

        }

        public List<UserDTO> GetAll()
        {
            var users = new List<UserDTO>();
            foreach (var user in uOW.UserRepo.All.ToList())
            {
                var User = uOW.UserRepo.GetByID(user.Id);
                users.Add(Mapper.Map<UserDTO>(User));
            }
            return users;
        }

        public UserDTO GetUser(string email, string password)
        {
            var user = uOW.UserRepo
                          .Get()
                          .FirstOrDefault(s => (s.Email == email && s.Password == password));

            // TODO: fix mapping - return user != null ? Mapper.Map<UserDTO>(user) : null;
            return user != null ? new UserDTO() { Id = user.Id, Email = user.Email, UserName = user.UserName } : null;
        }

        public void UpdateUser(int Id, string UserName, string Password, string Email, int RoleId)
        {
            var User = uOW.UserRepo.GetByID(Id);
            User.UserName = UserName;
            User.Password = Password;
            User.Email = Email;
            User.RoleId = RoleId;
            uOW.Save();
        }

        public void Insert(UserDTO user)
        {
            if (user == null) return;
            User dbUser = Mapper.Map<User>(user);
            dbUser.RoleId = user.RoleId == 0 ? 2 : dbUser.RoleId;
            uOW.UserRepo.Insert(dbUser);
            uOW.Save();
        }
    }
}
