using AutoMapper;
using ExtendedXmlSerialization;
using Model.DB;
using Model.DTO;
using System.Collections.Generic;
using BAL.Manager;

namespace BAL
{
	public class MappingProfile : Profile
	{
		private ExtendedXmlSerializer serializer = new ExtendedXmlSerializer();
		private SerializerForGrabber grabberSerializer = new SerializerForGrabber();

		protected override void Configure()
		{
			base.Configure();

			CreateMap<UserDTO, User>();
			CreateMap<User, UserDTO>()
				.ForMember(x => x.RoleName, y => y.MapFrom(t => t.Role.Name));

			CreateMap<NetworkUserDTO, User>();
			CreateMap<User, NetworkUserDTO>()
				.ForMember(x => x.RoleName, y => y.MapFrom(t => t.Role.Name));


			CreateMap<Category, CategoryDTO>()
				.ForMember(p => p.ChildrenCategory, m => m.MapFrom(t => new List<CategoryDTO>()));

			CreateMap<Role, RoleDTO>()
				.ForMember(p => p.Id, m => m.MapFrom(t => t.Id))
				.ForMember(p => p.Name, m => m.MapFrom(t => t.Name))
				.ForMember(p => p.Description, m => m.MapFrom(t => t.Description));

			CreateMap<Property, PropertyDTO>();

			CreateMap<PriceFollower, PriceFollowerDTO>();
			CreateMap<PriceFollowerDTO, PriceFollower>();
			CreateMap<WebShop, WebShopDTO>();
			CreateMap<WebShopDTO, WebShop>();

			CreateMap<ParserTask, ParserTaskDTO>()
				.ForMember(
				p => p.IteratorSettings,
				m => m.MapFrom(x => (IteratorSettingsDTO)serializer.Deserialize(x.IteratorSettings, typeof(IteratorSettingsDTO))))
				.ForMember(
				p => p.GrabberSettings,
				m => m.MapFrom(x => (GrabberSettingsDTO)grabberSerializer.Deserialize(x.GrabberSettings, typeof(GrabberSettingsDTO))));

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

			CreateMap<PropertyDTO, GrabberPropertyItemDTO>();

			CreateMap<Good, GoodDTO>()
				.ForMember(
				p => p.Category,
				m => m.Ignore()
				)
				.ForMember(
				p => p.WebShop,
				m => m.Ignore()
				)
				.ForMember(
				x => x.PropertyValues,
				m => m.MapFrom(
					t => (serializer.Deserialize(t.XmlData, typeof(PropertyValuesDTO)) as PropertyValuesDTO)
					)
				);

			CreateMap<GoodDTO, Good>()
				.ForMember(
				p => p.Category,
				m => m.Ignore()
				)
				.ForMember(
				p => p.WebShop,
				m => m.Ignore()
				)
				.ForMember(
				x => x.XmlData,
				y => y.MapFrom(
					t => serializer.Serialize(t.PropertyValues)
					)
				);
			CreateMap<PriceHistoryDTO, PriceHistory>();
			CreateMap<PriceHistory, PriceHistoryDTO>();

			CreateMap<ExecutingInfoDTO, ExecutingInfo>()
				.ForMember(
				p => p.ParserTask,
				m => m.Ignore()
				);

			CreateMap<ExecutingInfo, ExecutingInfoDTO>()
				.ForMember(
				p => p.ParserTask,
				m => m.Ignore()
				);

			CreateMap<CommentDTO, Comment>()
				.ForMember(
				p => p.User,
				m => m.Ignore()
				)
				.ForMember(
				p => p.Good,
				m => m.Ignore()
				);

			CreateMap<Comment, CommentDTO>()
				.ForMember(
				p => p.User,
				m => m.Ignore()
				)
				.ForMember(
				p => p.Good,
				m => m.Ignore()
				);

			CreateMap<AppSettingsDTO, AppSetting>();
		}
	}
}