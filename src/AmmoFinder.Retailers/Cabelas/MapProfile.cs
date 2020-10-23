using AmmoFinder.Parsers;
using AmmoFinder.Retailers.Cabelas.Models;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
using System;
using System.Text;

namespace AmmoFinder.Retailers.Cabelas
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Tuple<IDocument, AttributeData, InventoryData>, Product>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => GetName(src.Item1, src.Item2)))
                .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlElement>("h1.main_header").Text().GetBrand()))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Item1
                    .QuerySelector<IHtmlDivElement>($"div#product_longdescription_{src.Item2.productId}").Text().Trim()))
                .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => src.Item3 != null ? src.Item3.isInStock : !GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.OnlineAvailability").InnerHtml.Contains("out of stock", StringComparison.CurrentCultureIgnoreCase)))
                .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Item3.isInStock ? src.Item3.quantity : 0))
                .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => src.Item1
                    .QuerySelector<IHtmlDivElement>($"div#product_longdescription_{src.Item2.productId}").Text().GetCasing()))
                .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.CartridgeorGauge").Text().GetCaliber()))
                .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.Grain").Text().GetGrain()))
                .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.Quantity").Text().GetRoundCount()))
                .ForMember(dst => dst.RoundContainer, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlDivElement>("div.Quantity").Text().GetRoundContainer()))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => GetProductDetailsDiv(src.Item1, src.Item2)
                    .QuerySelector<IHtmlInputElement>($"input#ProductInfoPrice_{src.Item2.catentry_id}").Value.Replace("$", "")))
                .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => string.Concat(src.Item2.productId, "-", src.Item2.catentry_id)))
                .ForAllOtherMembers(opt => opt.Ignore());
        }

        private string GetName(IDocument document, AttributeData attribute)
        {
            var sb = new StringBuilder();
            sb.Append(document.QuerySelector<IHtmlElement>("h1.main_header").Text().Trim());

            var caliber = GetProductDetailsDiv(document, attribute).QuerySelector<IHtmlDivElement>("div.CartridgeorGauge")?.Text().GetCaliber();
            var grain = GetProductDetailsDiv(document, attribute).QuerySelector<IHtmlDivElement>("div.Grain")?.Text().GetGrain();
            var roundCount = GetProductDetailsDiv(document, attribute).QuerySelector<IHtmlDivElement>("div.Quantity")?.Text().GetRoundCount();

            if (!string.IsNullOrWhiteSpace(caliber))
            {
                sb.Append($" - {caliber}");
            }

            if (!string.IsNullOrWhiteSpace(grain))
            {
                sb.Append($" - {grain} Grain");
            }

            if (!string.IsNullOrWhiteSpace(roundCount))
            {
                sb.Append($" - {roundCount} Rounds");
            }

            return sb.ToString();
        }

        private IHtmlDivElement GetProductDetailsDiv(IDocument document, AttributeData attribute)
        {
            return document.QuerySelector<IHtmlDivElement>($"div#WC_Sku_List_Row_Content_{attribute.catentry_id}");
        }

    }
}
