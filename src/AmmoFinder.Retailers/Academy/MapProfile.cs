using AmmoFinder.Common.Models;
using AmmoFinder.Parsers;
using AmmoFinder.Retailers.Academy.Models;
using AutoMapper;
using System;
using System.Linq;

namespace AmmoFinder.Retailers.Academy
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductResponse, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Product.Product.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => GetDescription(src.Product.Product)))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => src.Inventory.Online.First().InventoryStatus != "OUT_OF_STOCK"))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => Convert.ToInt64(Math.Round(Convert.ToDouble(src.Inventory.Online.First().AvailableQuantity)))))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => Extension.BaseUrl + src.Product.Product.SeoUrl.Substring(1)))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Product.Product.ProductPrice.ListPrice))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Product.Product.Manufacturer.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => GetDescription(src.Product.Product).GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => GetDescription(src.Product.Product).GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => GetDescription(src.Product.Product).GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => GetDescription(src.Product.Product).GetRoundCount()))
                .ForMember(dst => dst.RoundContainer, opt => opt.MapFrom(src => GetDescription(src.Product.Product).GetRoundContainer()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Product.Product.Id))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private string GetDescription(Product product)
        {
            if (!string.IsNullOrWhiteSpace(product.LongDescription))
            {
                return product.LongDescription;
            }

            if (!string.IsNullOrWhiteSpace(product.ShortDescription))
            {
                return product.ShortDescription;
            }

            if(product.ProductSpecifications.FirstOrDefault()?.FeatureBenefits != null)
            {
                return string.Join(", ", product.ProductSpecifications.First().FeatureBenefits.Value);
            }

            return product.Name;
        }
    }
}
