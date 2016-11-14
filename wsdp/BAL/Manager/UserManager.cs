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
		/// Get user by email and password
		/// </summary>
		public UserDTO GetUser(string email, string password)
		{
			return GetAll().FirstOrDefault(x => x.Email == email && x.Password == password);
		}
        
        /// <summary>
        /// Get social network user from database from autogenerate uid password and network name  
        /// </summary>
        /// <param name="password">ULoginUser.Uid</param>
        /// <param name="network"></param>
        /// <returns></returns>
        public UserDTO GetSocialNetworkUser(string uId, string network)
        {
            return network != null ? GetAll().FirstOrDefault(x => x.Password == uId && x.SocialNetwork == network) : null;
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
		/// Insert User into database
		/// </summary>
		public void Insert(UserDTO user)
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
    }
}