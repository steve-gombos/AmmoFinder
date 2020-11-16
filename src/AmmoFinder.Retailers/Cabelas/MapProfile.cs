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
            CreateMap<Tuple<IDocument, MapperData>, Product>()
               .ForMember(dst => dst.Name, opt => opt.MapFrom(src => GetName(src.Item1, src.Item2.CatEntryId)))
               .ForMember(dst => dst.Description, opt => opt.MapFrom(src => 
                    src.Item1.QuerySelector<IHtmlDivElement>($"div#product_longdescription_{src.Item2.ProductId}").Text().Trim()))
               .ForMember(dst => dst.IsAvailable, opt => opt.MapFrom(src => 
                    src.Item2.Inventory != null ? src.Item2.Inventory.isInStock : !GetProductDetailsDiv(src.Item1, src.Item2.CatEntryId)
                        .QuerySelector<IHtmlDivElement>("div.OnlineAvailability").InnerHtml.Contains("out of stock", StringComparison.CurrentCultureIgnoreCase)))
               .ForMember(dst => dst.Inventory, opt => opt.MapFrom(src => src.Item2.Inventory.isInStock ? src.Item2.Inventory.quantity : 0))
               .ForMember(dst => dst.Price, opt => opt.MapFrom(src => 
                    GetProductDetailsDiv(src.Item1, src.Item2.CatEntryId).QuerySelector<IHtmlInputElement>($"input#ProductInfoPrice_{src.Item2.CatEntryId}").Value.Replace("$", "")))
               .ForMember(dst => dst.Url, opt => opt.MapFrom(src => src.Item2.ProductUrl))
               .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Item1.QuerySelector<IHtmlElement>("h1.main_header").Text().GetBrand()))
               .ForMember(dst => dst.Casing, opt => opt.MapFrom(src => 
                    src.Item1.QuerySelector<IHtmlDivElement>($"div#product_longdescription_{src.Item2.ProductId}").Text().GetCasing()))
               .ForMember(dst => dst.Caliber, opt => opt.MapFrom(src => 
                    GetProductDetailsDiv(src.Item1, src.Item2.CatEntryId).QuerySelector<IHtmlDivElement>("div.CartridgeorGauge").Text().GetCaliber()))
               .ForMember(dst => dst.Grain, opt => opt.MapFrom(src => 
                    GetProductDetailsDiv(src.Item1, src.Item2.CatEntryId).QuerySelector<IHtmlDivElement>("div.Grain").Text().GetGrain()))
               .ForMember(dst => dst.RoundCount, opt => opt.MapFrom(src => 
                    GetProductDetailsDiv(src.Item1, src.Item2.CatEntryId).QuerySelector<IHtmlDivElement>("div.Quantity").Text().GetRoundCount()))
               .ForMember(dst => dst.RetailerProductId, opt => opt.MapFrom(src => string.Concat(src.Item2.ProductId, "-", src.Item2.CatEntryId)))
               .ForAllOtherMembers(opt => opt.Ignore());
        }

        private string GetName(IDocument document, string catEntryId)
        {
            var sb = new StringBuilder();
            sb.Append(document.QuerySelector<IHtmlElement>("h1.main_header").Text().Trim());

            var caliber = GetProductDetailsDiv(document, catEntryId).QuerySelector<IHtmlDivElement>("div.CartridgeorGauge")?.Text().GetCaliber();
            var grain = GetProductDetailsDiv(document, catEntryId).QuerySelector<IHtmlDivElement>("div.Grain")?.Text().GetGrain();
            var roundCount = GetProductDetailsDiv(document, catEntryId).QuerySelector<IHtmlDivElement>("div.Quantity")?.Text().GetRoundCount();

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

        private IHtmlDivElement GetProductDetailsDiv(IDocument document, string catEntryId)
        {
            return document.QuerySelector<IHtmlDivElement>($"div#WC_Sku_List_Row_Content_{catEntryId}");
        }
    }
}
