using System;

namespace AmmoFinder.Common.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public bool IsAvailable { get; set; }
        public long Inventory { get; set; }
        public decimal Price { get; set; }
        public string RoundCount { get; set; }
        public string RoundType { get; set; }
        public string Caliber { get; set; }
        public string Casing { get; set; }
        public string Grain { get; set; }
        public string Url { get; set; }
        public string RetailerProductId { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
