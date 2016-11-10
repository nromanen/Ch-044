using AutoMapper;
using BAL.Interface;
using DAL.Interface;
using Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace BAL.Manager
{
    public class RoleManager : BaseManager, IRoleManager
    {
        public RoleManager(IUnitOfWork uOW)
            : base(uOW)
        {
        }

        public List<RoleDTO> GetAll()
        {
            var roles = new List<RoleDTO>();
            foreach (var role in uOW.RoleRepo.All.ToList())
            {
                var Role = uOW.RoleRepo.GetByID(role.Id);
                roles.Add(Mapper.Map<RoleDTO>(Role));
            }
            return roles;
        }
    }
}