using ECommerceProject.Entities.DomainModels;

namespace ECommerceProject.WebUI.Helper
{
   public interface ICartSessionHelper
   {
       Cart GetCart(string key);
       void SetCart(string key, Cart cart);
       void Clear();
   }
}
