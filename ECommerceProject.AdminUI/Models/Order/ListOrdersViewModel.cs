using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceProject.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceProject.AdminUI.Models.Order
{
    public class ListOrdersViewModel
    {
        public ListOrdersViewModel(Entities.Concrete.Order order)
        {
            OrderNumber = order.OrderNumber;
            Total = order.Total;
            OrderDate = order.OrderDate;
            OrderState = order.OrderState.ToString();
            UserName = order.UserName;
            EnumOrderStates = Enum.GetValues(typeof(EnumOrderState)).Cast<EnumOrderState>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
        }

        public ListOrdersViewModel()
        {
            
        }

      

        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderState { get; set; }
        public string UserName { get; set; }

        public IEnumerable<SelectListItem> EnumOrderStates { get; set; }

        //public virtual List<OrderLine> OrderLines { get; set; }
    }
}
