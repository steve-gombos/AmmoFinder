using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Parsers;
using AmmoFinder.Retailers.AimSurplus.Models;
using AngleSharp.Dom;
using AutoMapper;
using System;

namespace AmmoFinder.Retailers.AimSurplus
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Tuple<ProductDetail, string>, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1.Description))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => src.Item1.Inventory > 0))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Item1.Inventory))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Item1.Url))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Item1.Price))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Item1.Name.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.Item1.Description.GetCasing() ?? src.Item2.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.Item1.Name.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.Item1.Name.GetGrain() ?? src.Item1.Description.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.Item1.Name.GetRoundCount() ?? src.Item1.Description.GetRoundCount()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Item1.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Tuple<IDocument, string>, Product>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.QuerySelector("h1.productView-title").Text()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1.QuerySelector("div.full-description").Text().TrimExtra()))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => !string.Equals(src.Item1.QuerySelector("p.alertBox-message").QuerySelector("span").TextContent, "out of stock", StringComparison.CurrentCultureIgnoreCase)))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Item2))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(src.Item1.QuerySelector("span.price.price--withoutTax").TextContent.Replace("$", ""))))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => 
                    src.Item1.QuerySelector("h1.productView-title").Text().GetBrand() ??
                    src.Item1.QuerySelector("section.item-specs").Text().GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.productView-title").Text().GetCasing() ??
                    src.Item1.QuerySelector("section.item-specs").Text().GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.productView-title").Text().GetCaliber() ??
                    src.Item1.QuerySelector("section.item-specs").Text().GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.productView-title").Text().GetGrain() ??
                    src.Item1.QuerySelector("section.item-specs").Text().GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.productView-title").Text().GetRoundCount() ??
                    src.Item1.QuerySelector("section.item-specs").Text().GetRoundCount()))
                .ForMember(dst => dst.RoundType, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.productView-title").Text().GetBulletType() ??
                    src.Item1.QuerySelector("section.item-specs").Text().GetBulletType()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => new Uri(src.Item2).AbsolutePath.Substring(1).TrimTrailingSlash()))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
