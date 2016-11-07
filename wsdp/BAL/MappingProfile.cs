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
			CreateMap<User, UserDTO>();
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

			CreateMap<Parser, ParserDTO>()
				.ForMember(
				p => p.IteratorSettings, 
				m => m.MapFrom(x => (IteratorSettingsDTO)serializer.Deserialize(x.IteratorSettings, typeof(IteratorSettingsDTO))))
				.ForMember(
				p => p.GrabberSettings,
				m => m.MapFrom(x => (GrabberSettingsDTO)serializer.Deserialize(x.GrabberSettings, typeof(GrabberSettingsDTO))));

			CreateMap<ParserDTO, Parser>()
				.ForMember(
				p => p.IteratorSettings,
				m => m.MapFrom(x => serializer.Serialize(x.IteratorSettings)))
				.ForMember(
				p => p.GrabberSettings,
				m => m.MapFrom(x => serializer.Serialize(x.GrabberSettings)));
		}
    }
}
