using System.ComponentModel.DataAnnotations;

namespace ECommerceProject.Entities.Concrete
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}