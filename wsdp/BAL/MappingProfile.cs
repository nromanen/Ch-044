﻿using AutoMapper;
using Model.DB;
using Model.DTO;
using Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtendedXmlSerialization;

namespace BAL
{
    public class MappingProfile : Profile
    {
		private ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();

		protected override void Configure()
        {
            base.Configure();

            CreateMap<UserDTO, User>();
			CreateMap<User, UserDTO>();

            CreateMap<Role, RoleDTO>()
                .ForMember(p => p.Id, m => m.MapFrom(t => t.Id))
                .ForMember(p => p.Name, m => m.MapFrom(t => t.Name))
                .ForMember(p => p.Description, m => m.MapFrom(t => t.Description));

			CreateMap<TV, TVDTO>()
				.ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.ImageLink));

            CreateMap<ConcreteGood, PhoneSimpleDTO>()
                .ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.Good.ImgUrl))
                .ForMember(p => p.Name, m => m.MapFrom(t => t.Good.Name));

			CreateMap<Category, CategoryDTO>()
                .ForMember(p => p.ChildrenCategory,
                    m => m.MapFrom(t => new List<CategoryDTO>()));

			CreateMap<Fridge, FridgeDTO>(); 

			CreateMap<TapeRecorder, TapeRecorderDTO>()
				.ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.ImgPath));


			CreateMap<Property, PropertyDTO>();

            CreateMap<WebShop, WebShopDTO>();
            CreateMap<WebShopDTO, WebShop>();

			CreateMap<ParserTask, ParserTaskDTO>()
				.ForMember(
				p => p.IteratorSettings, 
				m => m.MapFrom(x => (IteratorSettingsDTO)serializer.Deserialize(x.IteratorSettings, typeof(IteratorSettingsDTO))))
				.ForMember(
				p => p.GrabberSettings,
				m => m.MapFrom(x => (GrabberSettingsDTO)serializer.Deserialize(x.GrabberSettings, typeof(GrabberSettingsDTO))));

            CreateMap<ParserTaskDTO, ParserTask>()
                .ForMember(
                p => p.IteratorSettings,
                m => m.MapFrom(x => (x.IteratorSettings != null ? serializer.Serialize(x.IteratorSettings) : null)))
                .ForMember(
                p => p.Category,
                m => m.Ignore()
                )
                .ForMember(
                p => p.WebShop,
                m => m.Ignore()
                )
                .ForMember(
                p => p.GrabberSettings,
                m => m.MapFrom(x => (x.GrabberSettings != null ? serializer.Serialize(x.GrabberSettings) : null)));


		}
    }
}
