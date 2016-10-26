using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Model.DTO;
using Model.DB;
using Model.Product;

namespace BAL
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(
                cfg => cfg.AddProfile(new MappingProfile())
                );

        }
    }
}
