﻿using System;
using System.Collections.Generic;

namespace SportStoreWiki.Models;

public partial class Order
{
    public int Id { get; set; }

    public string OrderList { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int PickupPoint { get; set; }

    public string Client { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; } = new List<OrderProduct>();

    public virtual PickupPoint PickupPointNavigation { get; set; } = null!;
}
