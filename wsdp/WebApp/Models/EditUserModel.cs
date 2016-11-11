using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class EditUserModel
    {
        public UserDTO User { get; set; }
        public List<RoleDTO> Roles { get; set; }

        public EditUserModel()
        {
            User = new UserDTO();
        }
    }
}