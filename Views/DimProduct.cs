using System;
using System.Collections.Generic;

namespace DataWarehouseApi.Views;

public partial class DimProduct
{
    public long? ProductKey { get; set; }

    public int? ProductId { get; set; }

    public string? ProductNumber { get; set; }

    public string? ProductName { get; set; }

    public int? Cost { get; set; }

    public string? ProductLine { get; set; }

    public string? CategoryId { get; set; }

    public string? Category { get; set; }

    public string? SubCategory { get; set; }

    public DateOnly? StartDate { get; set; }

    public string? Maintenance { get; set; }
}
