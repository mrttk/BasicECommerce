using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicECommerce.Models
{
    public class ShippingDetails
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string AddressTitle { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string PostCode { get; set; }

    }
}