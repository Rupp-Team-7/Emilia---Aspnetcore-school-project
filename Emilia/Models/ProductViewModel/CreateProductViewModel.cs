using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace Emilia.Models.ProductViewModel
{

    public class CreateProductViewModel
    {
        [MaxLength(12)]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        public Category Category { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        [Display(Name = "Unit prices")]
        public decimal UnitPrice { get; set; }

        [MaxLength(30)]
        [Display(Name = "Tags")]
        public string Tags { get; set; }

        [Display(Name = "Brand name")]
        public string BrandName { get; set; }
        public string Origin { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [MaxLength(150)]
        public string Specification { get; set; }

        public IFormFile Files { get; set; }

    }


}