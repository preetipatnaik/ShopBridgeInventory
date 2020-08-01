using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBridgeMVC.Models
{
    public class Products
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Enter Product Name.")]
        [DisplayName("Name ")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Enter Product Price.")]
        [DisplayName("Price ")]
        public decimal ? ProductPrice { get; set; }

        [Required(ErrorMessage = "Enter Product Description.")]
        [DisplayName("Description ")]
        public string ProductDesc { get; set; }

        [DisplayName("Upload Image ")]
        public string ProductImage { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        [DisplayName("Upload Image ")]  
        public string ImagePath { get; set; }  
  
        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class ProductSave
    {
        public int ProductId { get; set; }

        
        public string ProductName { get; set; }

        
        public decimal? ProductPrice { get; set; }

        
        public string ProductDesc { get; set; }

        
        public string ProductImage { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        
        public string ImagePath { get; set; }
        
    }

}