using SmartBusinessApp.Domain; // ← change to your actual namespace if BillDto is in different project
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace SmartBusinessApp.Infrastructure.Repositories
{
    public class BillRepository
    {
        private readonly string _connectionString;

        public BillRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int SaveBill(BillDto bill)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required);

            using var conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
            conn.Open();

            // 1. Insert Bill header and get generated BillId
            using var cmdBill = new Microsoft.Data.SqlClient.SqlCommand(
                @"INSERT INTO Bill (BillDate, BranchId, CustomerId, TotalGross, TotalTax, TotalNet)
                  OUTPUT INSERTED.BillId
                  VALUES (@BillDate, @BranchId, @CustomerId, @TotalGross, @TotalTax, @TotalNet)", conn);

            cmdBill.Parameters.AddWithValue("@BillDate", bill.BillDate);
            cmdBill.Parameters.AddWithValue("@BranchId", bill.BranchId);
            cmdBill.Parameters.AddWithValue("@CustomerId", bill.CustomerId);
            cmdBill.Parameters.AddWithValue("@TotalGross", bill.Items.Sum(i => i.Gross));
            cmdBill.Parameters.AddWithValue("@TotalTax", bill.Items.Sum(i => i.TaxAmount));
            cmdBill.Parameters.AddWithValue("@TotalNet", bill.Items.Sum(i => i.NetAmount));

            int billId = (int)cmdBill.ExecuteScalar();

            // 2. Insert each BillItem
            foreach (var item in bill.Items)
            {
                using var cmdItem = new Microsoft.Data.SqlClient.SqlCommand(
                    @"INSERT INTO BillItem (BillId, ProductId, Quantity, Rate, TaxPercent, Gross, TaxAmount, NetAmount)
                      VALUES (@BillId, @ProductId, @Quantity, @Rate, @TaxPercent, @Gross, @TaxAmount, @NetAmount)", conn);

                cmdItem.Parameters.AddWithValue("@BillId", billId);
                cmdItem.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmdItem.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmdItem.Parameters.AddWithValue("@Rate", item.Rate);
                cmdItem.Parameters.AddWithValue("@TaxPercent", item.TaxPercent);
                cmdItem.Parameters.AddWithValue("@Gross", item.Gross);
                cmdItem.Parameters.AddWithValue("@TaxAmount", item.TaxAmount);
                cmdItem.Parameters.AddWithValue("@NetAmount", item.NetAmount);

                cmdItem.ExecuteNonQuery();
            }

            scope.Complete();

            return billId;
        }
    }
}