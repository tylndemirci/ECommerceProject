﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ECommerceProject.Core.Enums
{
    public enum EnumOrderState
    {
        [Description("Order is waiting for approval")]WaitingForApproval,
        [Description("Order is approved")] OrderIsApproved,
        [Description("Order is on the way to your address")] OrderDispatched,
        [Description("Delivery day")] OutForDelivery,
        [Description("Order is completed")] OrderCompleted

    }
}
