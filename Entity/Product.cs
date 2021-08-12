using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicECommerce.Entity
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public bool IsHomePage { get; set; }
        public bool IsApproved { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}