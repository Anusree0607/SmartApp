using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SmartBusinessApp.Domain;
using SmartBusinessApp.UI;
using System.Collections.Generic;

namespace SmartBusinessApp.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        // Declare at class level – this is the key fix
        private readonly string _connectionString;

        // Constructor – receives configuration and sets the field
        public CustomerController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = new List<CustomerItem>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("SELECT CustomerId, Name FROM Customer", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(new CustomerItem
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            return Ok(customers);
        }
    }
}