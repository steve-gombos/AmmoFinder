using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AmmoFinder.Retailers.Cabelas.Models
{
    [ExcludeFromCodeCoverage]
    public class AttributeData
    {
        public string catentry_id { get; set; }
        public string buyable { get; set; }
        public string productId { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public string ItemImage { get; set; }
        public string ItemImage467 { get; set; }
        public string ItemThumbnailImage { get; set; }
    }
}
