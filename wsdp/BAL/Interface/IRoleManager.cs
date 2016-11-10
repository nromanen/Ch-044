using Model.DTO;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface IRoleManager
    {
        List<RoleDTO> GetAll();
    }
}