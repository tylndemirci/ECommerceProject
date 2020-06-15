using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceProject.Entities.Concrete
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required] public int CategoryId { get; set; }
        public int? ProductDetailsId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<ProductDetails> ProductDetails { get; set; }
        
        public int Count { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }
        public bool IsStock { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFeatured { get; set; } 
        public bool IsDeleted { get; set; }

        [Required] public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductColor { get; set; }
        public string ImageUrl { get; set; }
        
       
    }
}
