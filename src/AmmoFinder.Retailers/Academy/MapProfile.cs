using AmmoFinder.Common.Models;
using AmmoFinder.Parsers;
using AmmoFinder.Retailers.Academy.Models;
using AutoMapper;
using System.Linq;

namespace AmmoFinder.Retailers.Academy
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => true)) // Only get in stock
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => 0))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => Extension.BaseUrl + src.SeoUrl.Substring(1)))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.DefaultSkuPrice.ListPrice))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Name.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.ShortDescription.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.Name.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.Name.GetGrain() ?? src.ShortDescription.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.Name.GetRoundCount() ?? src.ShortDescription.GetRoundCount()))
                .ForMember(dst => dst.RoundContainer, opt => opt.MapFrom(src => src.Name.GetRoundContainer() ?? src.ShortDescription.GetRoundContainer()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<ProductResponse, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Product.Product.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Product.Product.LongDescription))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => true)) // Only get in stock
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Inventory.Online.First().AvailableQuantity ))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => Extension.BaseUrl + src.Product.Product.SeoUrl.Substring(1)))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Product.Product.ProductPrice.ListPrice))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Product.Product.Manufacturer.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.Product.Product.LongDescription.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.Product.Product.LongDescription.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.Product.Product.LongDescription.GetGrain() ?? src.Product.Product.Name.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.Product.Product.LongDescription.GetRoundCount() ?? src.Product.Product.Name.GetRoundCount()))
                .ForMember(dst => dst.RoundContainer, opt => opt.MapFrom(src => src.Product.Product.LongDescription.GetRoundContainer() ?? src.Product.Product.Name.GetRoundContainer()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Product.Product.Id))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
