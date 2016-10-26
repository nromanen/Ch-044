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

namespace BAL.Manager
{
    public class UserManager : BaseManager, IUserManager
    {
        static readonly ILog logger = LogManager.GetLogger("RollingLogFileAppender");
        public UserManager(IUnitOfWork uOW)
            : base(uOW)
        {

        }
		
		public UserDTO GetUser(string email, string password) {
			var user = uOW.UserRepo
						  .Get()
						  .FirstOrDefault(s => (s.Email == email && s.Password == password));

			// TODO: fix mapping - return user != null ? Mapper.Map<UserDTO>(user) : null;
			return user != null ? new UserDTO() { Id = user.Id, Email = user.Email, UserName = user.UserName} : null;
		}
	}
}
