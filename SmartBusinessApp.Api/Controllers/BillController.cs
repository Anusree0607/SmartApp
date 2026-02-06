using Microsoft.AspNetCore.Mvc;
using SmartBusinessApp.Domain;
using SmartBusinessApp.Infrastructure.Repositories;
using Microsoft.Data.SqlClient; 

namespace SmartBusinessApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BillRepository _repository;

        public BillController(BillRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult CreateBill([FromBody] BillDto bill)
        {
            if (bill == null || bill.Items == null || bill.Items.Count == 0)
            {
                return BadRequest("Invalid or empty bill data.");
            }

            try
            {
                int billId = _repository.SaveBill(bill);

                return Ok(new
                {
                    Message = "Bill saved successfully!",
                    BillId = billId
                });
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error saving bill: {ex.Message}");
            }
        }
    }
}