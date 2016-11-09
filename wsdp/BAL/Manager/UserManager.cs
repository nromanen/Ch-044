using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Manager
{
    public class UserManager : BaseManager, IUserManager
    {
        public UserManager(IUnitOfWork uOW)
            : base(uOW)
        {
        }

        /// <summary>
        /// Get all users from db.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Update User in database.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="Email"></param>
        /// <param name="RoleId"></param>
        public void UpdateUser(int Id, string UserName, string Password, string Email, int RoleId)
        {
            var User = uOW.UserRepo.GetByID(Id);
            User.UserName = UserName;
            User.Password = Password;
            User.Email = Email;
            User.RoleId = RoleId;
            uOW.Save();
        }

        /// <summary>
        /// Insert User to database
        /// </summary>
        /// <param name="user"></param>
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