using System;
using System.Collections.Generic;

namespace DataWarehouseApi.Views;

public partial class DimCustomer
{
    public long? CustomerKey { get; set; }

    public int? CustomerId { get; set; }

    public string? CustomerNumber { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public string? MaritalStatus { get; set; }

    public DateOnly? CreateDate { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Country { get; set; }
}
