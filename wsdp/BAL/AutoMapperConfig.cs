using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Model.DTO;
using Model.DB;



namespace BAL
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            //Simple examples of configuring automapper
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User>());
        }
    }
}
