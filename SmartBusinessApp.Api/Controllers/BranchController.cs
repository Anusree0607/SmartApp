using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SmartBusinessApp.Domain;


    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        private readonly string _connectionString;

        // Constructor – injects configuration and sets the field
        public BranchController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var branches = new List<BranchItem>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("SELECT BranchId, Name FROM Branch ORDER BY Name", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                branches.Add(new BranchItem
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            return Ok(branches);
        }
    }