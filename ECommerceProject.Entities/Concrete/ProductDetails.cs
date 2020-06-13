using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceProject.Entities.Concrete
{
   public class ProductDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; }
        [Required] public string ProductDetailTitle { get; set; }
        [Required] public string ProductDetailDescription { get; set; }
        
    }
}
