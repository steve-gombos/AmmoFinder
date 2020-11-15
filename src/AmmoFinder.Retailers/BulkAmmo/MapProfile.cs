using AmmoFinder.Common.Extensions;
using AmmoFinder.Parsers;
using AmmoFinder.Retailers.BulkAmmo.Models;
using AngleSharp.Dom;
using AutoMapper;
using System;

namespace AmmoFinder.Retailers.BulkAmmo
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Tuple<IDocument, string>, Product>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.QuerySelector("div.product-name").QuerySelector("h1").Text()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1.QuerySelector("div.product-description").QuerySelector("div.std").Text().TrimExtra()))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Item1.QuerySelector("span.stock-qty").Text())))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Item1.QuerySelector("span.stock-qty").Text()))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Item2))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(src.Item1.QuerySelector("span.price").Text().Replace("$", ""))))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("div.product-name").QuerySelector("h1").Text().GetBrand() ??
                    src.Item1.QuerySelector("div.product-description").QuerySelector("div.std").Text().GetBrand()))
                .ForMember(dst => dst.RoundType, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("div.product-name").QuerySelector("h1").Text().GetBulletType() ??
                    src.Item1.QuerySelector("div.product-description").QuerySelector("div.std").Text().GetBulletType()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("div.product-name").QuerySelector("h1").Text().GetCasing() ??
                    src.Item1.QuerySelector("div.product-description").QuerySelector("div.std").Text().GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("div.product-name").QuerySelector("h1").Text().GetCaliber() ??
                    src.Item1.QuerySelector("div.product-description").QuerySelector("div.std").Text().GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("div.product-name").QuerySelector("h1").Text().GetGrain() ??
                    src.Item1.QuerySelector("div.product-description").QuerySelector("div.std").Text().GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("div.product-name").QuerySelector("h1").Text().GetRoundCount() ??
                    src.Item1.QuerySelector("div.product-description").QuerySelector("div.std").Text().GetRoundCount()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => new Uri(src.Item2).AbsolutePath.Substring(1).TrimTrailingSlash()))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
