using AmmoFinder.Common.Models;
using AmmoFinder.Data.Models;
using AutoMapper;

namespace AmmoFinder.Persistence.Mappers
{
    public class PersistenceMapper : Profile
    {
        public PersistenceMapper()
        {
            CreateMap<ProductModel, Product>()
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<RetailerModel, Retailer>()
                .ReverseMap();
        }
    }
}
