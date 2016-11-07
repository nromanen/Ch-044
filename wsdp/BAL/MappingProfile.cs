using AutoMapper;
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
            CreateMap<User, UserDTO>()
                .ForMember(p => p.UserName, m => m.MapFrom(t => t.UserName))
                .ForMember(p => p.Email, m => m.MapFrom(t => t.Email))
                .ForMember(p => p.Password, m => m.MapFrom(t => t.Password))
                .ForMember(p => p.RoleId, m => m.MapFrom(t => t.RoleId));
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

			CreateMap<Fridge, FridgeDTO>(); ;

			CreateMap<TapeRecorder, TapeRecorderDTO>()
				.ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.ImgPath));


			CreateMap<Property, PropertyDTO>();

            CreateMap<WebShop, WebShopDTO>();
            CreateMap<WebShopDTO, WebShop>();

			CreateMap<Parser, ParserDTO>()
				.ForMember(
				p => p.IteratorSettings, 
				m => m.MapFrom(x => (IteratorSettingsDTO)serializer.Deserialize(x.IteratorSettings, typeof(IteratorSettingsDTO))));
        }
    }
}
