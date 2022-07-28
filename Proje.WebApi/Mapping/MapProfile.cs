using AutoMapper;
using Proje.Entities;
using Proje.Entities.Dtos;

namespace Proje.WebApi.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserCreateDto>();
            CreateMap<User, UserAuthDto>();
            CreateMap<Product, ProductListDto>();
            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<Product, ProductSaveDto>().ReverseMap();
            CreateMap<Category,CategoryDto>();
            
        }
    }
}
