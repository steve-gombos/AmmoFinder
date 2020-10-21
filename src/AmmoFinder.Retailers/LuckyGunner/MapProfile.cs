using AmmoFinder.Parsers;
using AmmoFinder.Retailers.LuckyGunner.Models;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;

namespace AmmoFinder.Retailers.LuckyGunner
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<IHtmlListItemElement, Product>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h3.product-name")
                    .QuerySelector<IHtmlSpanElement>("span").Text()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.QuerySelector<IHtmlDivElement>("div.desc.std").OuterHtml))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.QuerySelector<IHtmlSpanElement>("span.stock-qty").Text())))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.QuerySelector<IHtmlSpanElement>("span.stock-qty").Text()))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h3.product-name").QuerySelector<IHtmlAnchorElement>("a").Href))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(src.QuerySelector<IHtmlSpanElement>("span.regular-price").Text().Replace("$", ""))))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.QuerySelector<IHtmlDivElement>("div.desc.std").Text().GetCasing()))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h3.product-name")
                    .QuerySelector<IHtmlSpanElement>("span").Text().GetBrand()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h3.product-name")
                    .QuerySelector<IHtmlSpanElement>("span").Text().GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h3.product-name")
                    .QuerySelector<IHtmlSpanElement>("span").Text().GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h3.product-name")
                    .QuerySelector<IHtmlSpanElement>("span").Text().GetRoundCount() ?? src.QuerySelector<IHtmlDivElement>("div.desc.std").OuterHtml.GetRoundCount()))
                .ForMember(dst => dst.RoundContainer, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h3.product-name")
                    .QuerySelector<IHtmlSpanElement>("span").Text().GetRoundContainer() ?? src.QuerySelector<IHtmlDivElement>("div.desc.std").OuterHtml.GetRoundContainer()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.QuerySelector<IHtmlElement>("h3.product-name")
                    .QuerySelector<IHtmlSpanElement>("span").Text().Replace(" ", "-")))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

    }
}
