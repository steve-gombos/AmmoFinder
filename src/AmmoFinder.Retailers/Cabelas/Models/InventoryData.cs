using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AmmoFinder.Retailers.Cabelas.Models
{
    [ExcludeFromCodeCoverage]
    public class InventoryWrapper
    {
        public Dictionary<string, InventoryData> onlineInventory { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class InventoryData
    {
        public string status { get; set; }
        public string image { get; set; }
        public string altText { get; set; }
        public bool isDropShip { get; set; }
        public int quantity { get; set; }
        public string availableDate { get; set; }
        public bool isInStock
        {
            get
            {
                return !string.Equals(status, "Out of Stock", StringComparison.CurrentCultureIgnoreCase);
            }
        }
    }
}
