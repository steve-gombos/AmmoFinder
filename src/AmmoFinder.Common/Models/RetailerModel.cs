using System;
using System.Collections.Generic;

namespace AmmoFinder.Common.Models
{
    public class RetailerModel
    {
        public int RetailerId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        public IEnumerable<ProductModel> Products { get; set; }
    }
}
