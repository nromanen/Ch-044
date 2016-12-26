using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System;

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
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDTO GetById(int id)
        {
            var user = uOW.UserRepo.GetByID(id);
            if (user == null) return null;
            var result = Mapper.Map<UserDTO>(user);

            return result;
        }


        /// <summary>
        /// Get user by email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserDTO GetByEmail(string email, string password)
        {
            var user = uOW.UserRepo.All.Where(x=>x.Network==null)
                .FirstOrDefault(x => x.Email == email && x.Password == password);
            return Mapper.Map<UserDTO>(user);
        }
        /// <summary>
        /// Get user by userName and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserDTO GetByUserName(string userName, string password)
        {
            var user = uOW.UserRepo.All.Where(x => x.Network == null)
                .FirstOrDefault(x => x.UserName == userName && x.Password == password);
            return Mapper.Map<UserDTO>(user);
        }

		public string GetEmail(int Id)
		{
			var email = uOW.UserRepo.GetByID(Id).Email;
			return email;
		}

        /// <summary>
        /// Get user from database by networkAccountId and network
        /// </summary>
        /// <param name="networkAccountId"></param>
        /// <param name="network"></param>
        /// <returns></returns>
	    public NetworkUserDTO GetNetworkUser(string networkAccountId, string network)
        {
            var user = uOW.UserRepo.All.FirstOrDefault(x => x.NetworkAccountId == networkAccountId && x.Network == network);
            return Mapper.Map<NetworkUserDTO>(user);
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
        /// Insert user into database
        /// </summary>
        /// <param name="user">UserDTO</param>
        public void Insert(UserDTO user)
        {
            if (user == null) return;
            User dbUser = Mapper.Map<User>(user);
            dbUser.RoleId = user.RoleId == 0 ? 2 : dbUser.RoleId;
            uOW.UserRepo.Insert(dbUser);
            uOW.Save();
        }
        /// <summary>
        /// Insert user into database
        /// </summary>
        /// <param name="user">NetworkUserDTO</param>
        public void Insert(NetworkUserDTO user)
        {
            if (user == null) return;
            User dbUser = Mapper.Map<User>(user);
            dbUser.RoleId = user.RoleId == 0 ? 2 : dbUser.RoleId;
            uOW.UserRepo.Insert(dbUser);
            uOW.Save();
        }

        /// <summary>
        /// Check the email's exictance in database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
		public bool EmailIsExist(string email)
        {
            return uOW.UserRepo.All.Any(x => x.Email == email);
        }

		public List<UserDTO> GetUsersSize(int skip,int pageSize)
		{
			var users = new List<UserDTO>();
			var data=uOW.UserRepo.All.OrderBy(i=>i.Id).Skip(skip).Take(pageSize).ToList();
			foreach (var user in data)
			{
				var User = uOW.UserRepo.GetByID(user.Id);
				users.Add(Mapper.Map<UserDTO>(User));
			}
			return users;
		}
		public int GetUsersSize()
		{
			var size = uOW.UserRepo.All.Count();
			return size;
		}
        /// <summary>
        /// Check the occount existance by network account id and network
        /// </summary>
        /// <param name="networkAccountId"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        public bool NetworkAccountExict(string networkAccountId, string network)
        {
            return uOW.UserRepo.All.Any(x => x.NetworkAccountId == networkAccountId && x.Network == network);
        }
    }
}