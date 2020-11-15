using AmmoFinder.Common.Extensions;
using AmmoFinder.Parsers;
using AmmoFinder.Retailers.SportsmansGuide.Models;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
using System;
using System.Web;

namespace AmmoFinder.Retailers.SportsmansGuide
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Tuple<IDocument, string>, Product>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.QuerySelector("h1").Text()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1.QuerySelector("div.key-features").OuterHtml))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => true)) // We only get in stock items back
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Item2))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Item1.QuerySelector("div.regular-price")
                    .QuerySelector("span.price").TextContent.GetDecimalValueInRange(true)))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => 
                    src.Item1.QuerySelector("h1").Text().GetBrand() ??
                    src.Item1.QuerySelector("div.key-features").TextContent.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1").Text().GetCasing() ??
                    src.Item1.QuerySelector("div.key-features").TextContent.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => 
                    src.Item1.QuerySelector("h1").Text().GetCaliber() ??
                    src.Item1.QuerySelector("div.key-features").TextContent.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => 
                    src.Item1.QuerySelector("h1").Text().GetGrain() ??
                    src.Item1.QuerySelector("div.key-features").TextContent.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => 
                    src.Item1.QuerySelector("h1").Text().GetRoundCount() ??
                    src.Item1.QuerySelector("div.key-features").TextContent.GetRoundCount()))
                .ForMember(dst => dst.RoundType, opt => opt.MapFrom(src =>
                    src.Item1.QuerySelector("h1").Text().GetBulletType() ??
                    src.Item1.QuerySelector("div.key-features").TextContent.GetBulletType()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => HttpUtility.ParseQueryString(new Uri(src.Item2).Query).Get("AdId")))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

    }
}
