using AmmoFinder.Common.Models;
using AmmoFinder.Parsers;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
using System;

namespace AmmoFinder.Retailers.BulkAmmo
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<IHtmlListItemElement, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Text))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Text))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.QuerySelector<IHtmlSpanElement>("span.stock-qty").Text())))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.QuerySelector<IHtmlSpanElement>("span.stock-qty").Text()))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Href))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(src.QuerySelector<IHtmlSpanElement>("span.price").Text().Replace("$", ""))))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetRoundCount()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.Replace(" ", "-")))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<Tuple<IHtmlListItemElement, string>, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Text))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Text))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Item1.QuerySelector<IHtmlSpanElement>("span.stock-qty").Text())))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlSpanElement>("span.stock-qty").Text()))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Href))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(src.Item1.QuerySelector<IHtmlSpanElement>("span.price").Text().Replace("$", ""))))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetCasing() ?? src.Item2.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetCaliber() ?? src.Item2.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetGrain() ?? src.Item2.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.GetRoundCount() ?? src.Item2.GetRoundCount()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlAnchorElement>("a.product-name").Text.Replace(" ", "-")))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

    }
}
