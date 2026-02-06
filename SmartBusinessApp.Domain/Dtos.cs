using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBusinessApp.Domain
{
    public class BranchItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CustomerItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxPercent { get; set; }
    }

    public class BillDto
    {
        public DateTime BillDate { get; set; }
        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        public List<BillItemDto> Items { get; set; } = new();
    }

    public class BillItemDto
    {
        public int ProductId { get; set; }
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
