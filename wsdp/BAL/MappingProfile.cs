using AutoMapper;
using Model.DB;
using Model.DTO;
using Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL {
	public class MappingProfile : Profile {
		protected override void Configure() {
			base.Configure();

			CreateMap<UserDTO, User>();
			CreateMap<User, UserDTO>();
			CreateMap<TV, TVDTO>()
				.ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.ImageLink))
				.ForMember(p => p.Name, m => m.MapFrom(t => t.Name))
				.ForMember(p => p.Price, m => m.MapFrom(t => t.Price));
			CreateMap<ConcreteGood, PhoneSimpleDTO>()
				.ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.Good.ImgUrl))
				.ForMember(p => p.Name, m => m.MapFrom(t => t.Good.Name));
			CreateMap<Category, CategoryDTO>()
				.ForMember(p => p.ChildrenCategory,
					m => m.MapFrom(t => new List<CategoryDTO>()));

			CreateMap<Fridge, FridgeDTO>().ForMember(p => p.ImagePath, m => m.MapFrom(t => t.ImagePath))
				.ForMember(p => p.Name, m => m.MapFrom(t => t.Name))
				.ForMember(p => p.Price, m => m.MapFrom(t => t.Price));

			CreateMap<Microwave, MicrowaveDTO>()
				.ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.ImgPath))
				.ForMember(p => p.Name, m => m.MapFrom(t => t.Name))
				.ForMember(p => p.Price, m => m.MapFrom(t => t.Price))
				.ForMember(p => p.urlPath, m => m.MapFrom(t => t.urlPath));


			CreateMap<Property, PropertyDTO>()
				.ForMember(p => p.Id, m => m.MapFrom(t => t.Id))
				.ForMember(p => p.Name, m => m.MapFrom(t => t.Name))
				.ForMember(p => p.Description, m => m.MapFrom(t => t.Description))
				.ForMember(p => p.Type, m => m.MapFrom(t => t.Type))
				.ForMember(p => p.Prefix, m => m.MapFrom(t => t.Prefix))
				.ForMember(p => p.Sufix, m => m.MapFrom(t => t.Sufix))
				.ForMember(p => p.Characteristic_Id, m => m.MapFrom(t => t.Characteristic_Id))
				.ForMember(p => p.Category_Id, m => m.MapFrom(t => t.Category_Id))
				.ForMember(p => p.DefaultValue, m => m.MapFrom(t => t.DefaultValue));
		}
	}
}
