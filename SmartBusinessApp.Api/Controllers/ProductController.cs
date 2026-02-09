using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SmartBusinessApp.Domain;
using SmartBusinessApp.Infrastructure.Repositories;
using SmartBusinessApp.UI;
using System.Collections.Generic;

namespace SmartBusinessApp.Api.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly string?_connectionString;

    public ProductController(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    [HttpGet]
    public IActionResult Get()
    {
        var products = new List<ProductItem>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT ProductId, Name, Rate, TaxPercent FROM Product", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            products.Add(new ProductItem
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(1),
                Rate = reader.GetDecimal(2),
                TaxPercent = reader.GetDecimal(3)
            });
        }
        return Ok(products);
    }
}
}