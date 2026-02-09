using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBusinessApp.Domain
{
    public class BranchItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CustomerItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductItem
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxPercent { get; set; }
    }

    public class BillDto
    {
        public DateTime BillDate { get; set; }
        public Guid BranchId { get; set; }
        public Guid CustomerId { get; set; }
        public List<BillItemDto> Items { get; set; } = new();
    }

    public class BillItemDto
    {
        public Guid  ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxPercent { get; set; }
        public decimal Gross { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
    internal class Dtos
    {
        
    }
}
