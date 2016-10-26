﻿using System;
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
            //Simple examples of configuring automapper
            Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>());
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, Category>());
            Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User>());
            Mapper.Initialize(cfg => cfg.CreateMap<ConcreteGood, PhoneSimpleDTO>()
                .ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.Good.ImgUrl))
                .ForMember(p => p.Name, m => m.MapFrom(t => t.Good.Name)));

            Mapper.Initialize(cfg => cfg.CreateMap<Fridge, FridgeDTO>().ForMember(p => p.ImagePath, m => m.MapFrom(t => t.ImagePath))
            .ForMember(p => p.Name, m => m.MapFrom(t => t.Name))
            .ForMember(p=>p.Price, m=>m.MapFrom(t=>t.Price)));

        }
    }
}
