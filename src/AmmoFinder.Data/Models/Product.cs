using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmmoFinder.Data.Models
{
    public class Product
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public bool IsAvailable { get; set; }

        public long Inventory { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string RoundCount { get; set; }

        public string RoundType { get; set; }

        public string Caliber { get; set; }

        public string Casing { get; set; }

        public string Grain { get; set; }

        public string Url { get; set; }

        public string RetailerProductId { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;


        public virtual Retailer Retailer { get; set; }
    }
}
