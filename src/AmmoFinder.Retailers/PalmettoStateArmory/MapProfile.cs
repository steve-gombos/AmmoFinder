using AmmoFinder.Parsers;
using AmmoFinder.Retailers.PalmettoStateArmory.Models;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;

namespace AmmoFinder.Retailers.PalmettoStateArmory
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<IHtmlListItemElement, Product>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-item-link").Text().Trim()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-item-link").Text().Trim()))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => true)) // We only get in stock products
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.QuerySelector<IHtmlSpanElement>("span.stock-qty").Text()))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-item-link").Href))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(src.QuerySelector<IHtmlSpanElement>("span.price").Text().Replace("$", ""))))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.QuerySelector<IHtmlDivElement>("div.desc.std").Text().GetCasing()))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-item-link").Text().Trim().GetBrand()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-item-link").Text().Trim().GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-item-link").Text().Trim().GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-item-link").Text().Trim().GetRoundCount()))
                .ForMember(dst => dst.RoundContainer, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-item-link").Text().Trim().GetRoundContainer()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

    }
}
