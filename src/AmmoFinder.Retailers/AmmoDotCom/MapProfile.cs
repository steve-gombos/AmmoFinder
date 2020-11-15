using AmmoFinder.Parsers;
using AmmoFinder.Retailers.AmmoDotCom.Models;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;

namespace AmmoFinder.Retailers.AmmoDotCom
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<IHtmlListItemElement, Product>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h2.product-name").Text().Trim()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.QuerySelector<IHtmlDivElement>("div.product-attributes").TextContent))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => src.QuerySelector<IHtmlSpanElement>("span.availability-label")
                    .Text().Contains("in stock", System.StringComparison.CurrentCultureIgnoreCase)))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.QuerySelector<IHtmlSpanElement>("span.availability-count").Text()))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h2.product-name").QuerySelector<IHtmlAnchorElement>("a").Href))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(src.QuerySelector<IHtmlSpanElement>("span.price").Text().Replace("$", ""))))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.QuerySelector<IHtmlDivElement>("div.product-attributes").TextContent.GetCasing()))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.QuerySelector<IHtmlDivElement>("div.product-attributes").TextContent.GetBrand()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h2.product-name").Text().Trim().GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.QuerySelector<IHtmlDivElement>("div.product-attributes").TextContent.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.QuerySelector<IHtmlDivElement>("div.product-attributes").TextContent.GetRoundCount()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

    }
}
