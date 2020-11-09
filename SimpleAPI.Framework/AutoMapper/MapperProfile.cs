using AutoMapper;

namespace SimpleAPI.Framework.AutoMapper
{
    public class MapperProfile : Profile
    {
        public void CreateMapReverse<TSource, TDestination1, TDestination2>()
        {
            CreateMap<TSource, TDestination1>().ReverseMap();
            CreateMap<TSource, TDestination2>().ReverseMap();
        }
    }
}
