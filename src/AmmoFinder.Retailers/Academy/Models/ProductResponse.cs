using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace AmmoFinder.Retailers.Academy.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductsResponse
    {
        [JsonPropertyName("recordSetTotal")]
        [JsonConverter(typeof(LongConverter))]
        public long RecordSetTotal { get; set; }

        [JsonPropertyName("recordSetCount")]
        [JsonConverter(typeof(LongConverter))]
        public long RecordSetCount { get; set; }

        [JsonPropertyName("productinfo")]
        public List<Product> Products { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ProductResponse
    {

        [JsonPropertyName("inventory")]
        public InventoryWrapper Inventory { get; set; }

        [JsonPropertyName("product-info")]
        public ProductWrapper Product { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class InventoryWrapper
    {
        [JsonPropertyName("online")]
        public List<InventoryData> Online { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class ProductWrapper
    {
        [JsonPropertyName("productinfo")]
        public Product Product { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class InventoryData
    {
        [JsonPropertyName("skuId")]
        [JsonConverter(typeof(LongConverter))]
        public long SkuId { get; set; }

        [JsonPropertyName("availableQuantity")]
        public string AvailableQuantity { get; set; }

        [JsonPropertyName("ecomCode")]
        public string EcomCode { get; set; }

        [JsonPropertyName("inventoryStatus")]
        public string InventoryStatus { get; set; }

        [JsonPropertyName("deliveryMessage")]
        public DeliveryMessage DeliveryMessage { get; set; }

        [JsonPropertyName("isClearance")]
        public string IsClearance { get; set; }

        [JsonPropertyName("addToCart")]
        [JsonConverter(typeof(BoolConverter))]
        public bool AddToCart { get; set; }

        [JsonPropertyName("isSafetyStockEnabled")]
        public string IsSafetyStockEnabled { get; set; }

        [JsonPropertyName("onlineStoreName")]
        public string OnlineStoreName { get; set; }

        [JsonPropertyName("availabilityDate")]
        public string AvailabilityDate { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class DeliveryMessage
    {
        [JsonPropertyName("onlineDeliveryMessage")]
        public OnlineDeliveryMessage OnlineDeliveryMessage { get; set; }

        [JsonPropertyName("storeDeliveryMessage")]
        public StoreDeliveryMessage StoreDeliveryMessage { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class OnlineDeliveryMessage
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("additionalInfoKey")]
        public string AdditionalInfoKey { get; set; }

        [JsonPropertyName("additionalInfoValue")]
        public string AdditionalInfoValue { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("showTick")]
        [JsonConverter(typeof(BoolConverter))]
        public bool ShowTick { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class StoreDeliveryMessage
    {
        [JsonPropertyName("storeInvType")]
        public string StoreInvType { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("showTick")]
        [JsonConverter(typeof(BoolConverter))]
        public bool ShowTick { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Product
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        [JsonConverter(typeof(LongConverter))]
        public long Id { get; set; }

        [JsonPropertyName("sellable")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Sellable { get; set; }

        [JsonPropertyName("varianttype")]
        public string Varianttype { get; set; }

        [JsonPropertyName("isGiftCard")]
        public string IsGiftCard { get; set; }

        [JsonPropertyName("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonPropertyName("defaultSku")]
        public string DefaultSku { get; set; }

        [JsonPropertyName("isSingleSkuProduct")]
        public bool IsSingleSkuProduct { get; set; }

        [JsonPropertyName("partNumber")]
        public string PartNumber { get; set; }

        [JsonPropertyName("seoURL")]
        public string SeoUrl { get; set; }

        [JsonPropertyName("imageURL")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("imageAltDescription")]
        public string ImageAltDescription { get; set; }

        [JsonPropertyName("defaultSkuPrice")]
        public Pricing DefaultSkuPrice { get; set; }

        [JsonPropertyName("storeId")]
        public string StoreId { get; set; }

        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonPropertyName("isBuyNowEligible")]
        public string IsBuyNowEligible { get; set; }

        [JsonPropertyName("longDescription")]
        public string LongDescription { get; set; }

        [JsonPropertyName("categoryId")]
        [JsonConverter(typeof(LongConverter))]
        public long CategoryId { get; set; }

        [JsonPropertyName("parentCategoryURL")]
        public string ParentCategoryUrl { get; set; }

        [JsonPropertyName("xfullimage")]
        public string Xfullimage { get; set; }

        [JsonPropertyName("fullImage")]
        public string FullImage { get; set; }

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonPropertyName("productAttrGroups")]
        public object[] ProductAttrGroups { get; set; }

        [JsonPropertyName("wgFlag")]
        public string WgFlag { get; set; }

        [JsonPropertyName("productPrice")]
        public Pricing ProductPrice { get; set; }

        [JsonPropertyName("productSpecifications")]
        public List<ProductSpecification> ProductSpecifications { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Pricing
    {
        [JsonPropertyName("salePrice")]
        public string SalePrice { get; set; }

        [JsonPropertyName("priceMessage")]
        public string PriceMessage { get; set; }

        [JsonPropertyName("savings")]
        public string Savings { get; set; }

        [JsonPropertyName("listPrice")]
        public string ListPrice { get; set; }

        [JsonPropertyName("pricePerUnit")]
        public string PricePerUnit { get; set; }

        [JsonPropertyName("ppuMsg")]
        public string PpuMsg { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class ProductSpecification
    {
        [JsonPropertyName("featureBenefits")]
        public FeatureBenefits FeatureBenefits { get; set; }

        [JsonPropertyName("whatsInTheBox")]
        public FeatureBenefits WhatsInTheBox { get; set; }

        [JsonPropertyName("legalDisclaimer")]
        public FeatureBenefits LegalDisclaimer { get; set; }

        [JsonPropertyName("stateRestriction")]
        public FeatureBenefits StateRestriction { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public partial class FeatureBenefits
    {
        [JsonPropertyName("value")]
        public string[] Value { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}
