using System;
using System.Collections.Generic;

namespace DataWarehouseApi.Views;

public partial class FactOrder
{
    public string? OrderNumber { get; set; }

    public long? CustomerKey { get; set; }

    public long? ProductKey { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? ShippingDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public int? Sales { get; set; }

    public int? Quantity { get; set; }

    public int? Price { get; set; }
}
