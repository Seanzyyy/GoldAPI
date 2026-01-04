using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouseApi.Views;

public partial class Goldcontext : DbContext
{
    public Goldcontext()
    {
    }

    public Goldcontext(DbContextOptions<Goldcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<DimCustomer> DimCustomers { get; set; }

    public virtual DbSet<DimProduct> DimProducts { get; set; }

    public virtual DbSet<FactOrder> FactOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-736BP3F\\SQLEXPRESS;Database=DataWarehouse;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DimCustomer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("dim_customers", "gold");

            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
            entity.Property(e => e.CreateDate).HasColumnName("create_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerKey).HasColumnName("customer_key");
            entity.Property(e => e.CustomerNumber)
                .HasMaxLength(50)
                .HasColumnName("customer_number");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("marital_status");
        });

        modelBuilder.Entity<DimProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("dim_products", "gold");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .HasColumnName("category_id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Maintenance)
                .HasMaxLength(50)
                .HasColumnName("maintenance");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductKey).HasColumnName("product_key");
            entity.Property(e => e.ProductLine)
                .HasMaxLength(50)
                .HasColumnName("product_line");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductNumber)
                .HasMaxLength(50)
                .HasColumnName("product_number");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.SubCategory)
                .HasMaxLength(50)
                .HasColumnName("sub-category");
        });

        modelBuilder.Entity<FactOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("fact_orders", "gold");

            entity.Property(e => e.CustomerKey).HasColumnName("customer_key");
            entity.Property(e => e.DueDate).HasColumnName("due_date");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .HasColumnName("order_number");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductKey).HasColumnName("product_key");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Sales).HasColumnName("sales");
            entity.Property(e => e.ShippingDate).HasColumnName("shipping_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
