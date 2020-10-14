using AmmoFinder.Common.Models;
using AmmoFinder.Parsers;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
using System;
using System.Linq;

namespace AmmoFinder.Retailers.Cabelas
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Tuple<IHtmlListItemElement, AttributeData>, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => GetProductDiv(src.Item1).QuerySelector<IHtmlAnchorElement>("a").Text))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => GetProductDiv(src.Item1).QuerySelector<IHtmlAnchorElement>("a").Text))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => src.Item2.buyable))
                .ForMember(dst => dst.Url, opt => opt.MapFrom(src => GetProductDiv(src.Item1).QuerySelector<IHtmlAnchorElement>("a").Href))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => decimal.Parse(GetProductDiv(src.Item1).QuerySelector<IHtmlSpanElement>("span[itemprop=price]").Text().Replace("$", ""))))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => GetProductDiv(src.Item1).QuerySelector<IHtmlAnchorElement>("a").Text.GetBrand()))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => GetProductDiv(src.Item1).QuerySelector<IHtmlAnchorElement>("a").Text.GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => src.Item2.Attributes.First().Key.GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => GetProductDiv(src.Item1).QuerySelector<IHtmlAnchorElement>("a").Text.GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => GetProductDiv(src.Item1).QuerySelector<IHtmlAnchorElement>("a").Text.GetRoundCount()))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => GetProductDiv(src.Item1).QuerySelector<IHtmlAnchorElement>("a").Text))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private IHtmlDivElement GetProductDiv(IHtmlListItemElement listItem)
        {
            return listItem.QuerySelector<IHtmlDivElement>("div.product_name");
        }

    }
}
