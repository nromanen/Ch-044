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
            /*Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>()
            .ForMember(p => p.ChildrenCategory,
            m => m.MapFrom(
                          t => Mapper.Map<ICollection<Category>, ICollection<CategoryDTO>>(t.ChildrenCategory)
                          )
            ).MaxDepth(2));
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, Category>()
            .ForMember(p => p.ChildrenCategory, 
            m => m.MapFrom(
                          t => Mapper.Map<ICollection<CategoryDTO>, ICollection<Category>>(t.ChildrenCategory)
                          )
            ).MaxDepth(2));*/

            Mapper.Initialize(
                cfg => cfg.AddProfile(new MappingProfile())
                );
            

            //Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, User>());
            //Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
            
            /*Mapper.Initialize(cfg => cfg.CreateMap<ConcreteGood, PhoneSimpleDTO>()
                .ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.Good.ImgUrl))
                .ForMember(p => p.Name, m => m.MapFrom(t => t.Good.Name)));
            Mapper.Initialize(cfg => cfg.CreateMap<TV, TVDTO>()
                .ForMember(p => p.ImgUrl, m => m.MapFrom(t => t.ImageLink))
                .ForMember(p => p.Name, m => m.MapFrom(t => t.Name))
                .ForMember(p => p.Price, m => m.MapFrom(t => t.Price)));*/

            var mmm1 = Mapper.Map<UserDTO>(new User() { Email = "sdfsdf", Id = 2, Password = "sdfsdf"});
            var mmm2 = Mapper.Map<User>(new UserDTO() { Email = "sdfsdf", Id = 2});


            //Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>()
            //.ForMember( p => p.ChildrenCategory,
            //            m => m.MapFrom(t => new List<CategoryDTO>())));
            /*Mapper.Initialize(cfg => cfg.CreateMap<Category, CategoryDTO>()
            .ForMember(p => p.ChildrenCategory,
            m => m.MapFrom(
                          t => Mapper.Map<ICollection<Category>, ICollection<CategoryDTO>>(t.ChildrenCategory)
                          )
            ));*/

            /*Mapper.Initialize(cfg => cfg.CreateMap<CategoryDTO, Category>()
            .ForMember(p => p.ChildrenCategory,
            m => m.MapFrom(
                          t => Mapper.Map<ICollection<CategoryDTO>, ICollection<Category>>(t.ChildrenCategory)
                          )
            ).MaxDepth(2));*/
        }
    }
}
