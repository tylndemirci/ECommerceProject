using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Entities.Concrete
{
   public class ProductDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; }
        public string ProductDetailTitle { get; set; }
        public string ProductDetailDescription { get; set; }
        
    }
}
