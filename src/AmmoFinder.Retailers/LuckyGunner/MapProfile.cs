using AmmoFinder.Common.Extensions;
using AmmoFinder.Parsers;
using AmmoFinder.Retailers.LuckyGunner.Models;
using AngleSharp.Dom;
using AutoMapper;
using System;

namespace AmmoFinder.Retailers.LuckyGunner
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Tuple<IDocument, string>, Product>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.QuerySelector("h1.product-name").Text()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1.QuerySelector("div.product-section-details").QuerySelector("div.std").Text().TrimExtra()))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => true)) // We only get in stock items back
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Item1.QuerySelector("span.stock-qty").TextContent))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Item2))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(src.Item1.QuerySelector("span.regular-price").TextContent.Replace("$", ""))))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.product-name").Text().GetBrand() ??
                    src.Item1.QuerySelector("table.product-attribute-specs-table").Text().GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.product-name").Text().GetCasing() ??
                    src.Item1.QuerySelector("table.product-attribute-specs-table").Text().GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.product-name").Text().GetCaliber() ??
                    src.Item1.QuerySelector("table.product-attribute-specs-table").Text().GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.product-name").Text().GetGrain() ??
                    src.Item1.QuerySelector("table.product-attribute-specs-table").Text().GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.product-name").Text().GetRoundCount() ??
                    src.Item1.QuerySelector("table.product-attribute-specs-table").Text().GetRoundCount()))
                .ForMember(dst => dst.RoundType, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1.product-name").Text().GetBulletType() ??
                    src.Item1.QuerySelector("table.product-attribute-specs-table").Text().GetBulletType()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => new Uri(src.Item2).AbsolutePath.Substring(1).TrimTrailingSlash()))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

    }
}
