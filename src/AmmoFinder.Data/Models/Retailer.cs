using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmmoFinder.Data.Models
{
    public class Retailer
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RetailerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public virtual ICollection<Product> Products { get; set; }
    }
}
