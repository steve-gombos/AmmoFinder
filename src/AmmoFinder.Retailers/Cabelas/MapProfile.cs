using AmmoFinder.Common.Models;
using AmmoFinder.Parsers;
using AmmoFinder.Retailers.Cabelas.Models;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
using System;
using System.Linq;

namespace AmmoFinder.Retailers.Cabelas
{
    public class MapProfile : Profile
    {
        public MapProfile()
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

            CreateMap<Tuple<IDocument, AttributeData>, ProductModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlElement>("h1.main_header").Text()))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlElement>("h1.main_header").Text().GetBrand()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1
                    .QuerySelector<IHtmlDivElement>($"div#product_longdescription_{src.Item2.productId}").Text()))
                //TODO: Fix this property.  Reporting invalid availability
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => !GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.OnlineAvailability").Text().Contains("out of stock", StringComparison.CurrentCultureIgnoreCase)))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.Item1
                    .QuerySelector<IHtmlDivElement>($"div#product_longdescription_{src.Item2.productId}").Text().GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.CartridgeorGauge").Text().GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.Grain").Text().GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.Quantity").Text().GetRoundCount()))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlInputElement>($"input#ProductInfoPrice_{src.Item2.catentry_id}").Value.Replace("$", "")))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => src.Item2.productId))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private IHtmlDivElement GetProductDiv(IHtmlListItemElement listItem)
        {
            return listItem.QuerySelector<IHtmlDivElement>("div.product_name");
        }

        private IHtmlDivElement GetProductDetailsDiv(IDocument document, AttributeData attribute)
        {
            return document.QuerySelector<IHtmlDivElement>($"div#WC_Sku_List_Row_Content_{attribute.catentry_id}");
        }

    }
}
