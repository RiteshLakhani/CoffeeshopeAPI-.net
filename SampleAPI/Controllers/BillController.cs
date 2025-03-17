using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Model;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BillRepository _billRepository;

        public BillController(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        #region Get All Bills
        [HttpGet]
        [Route("GetBills")]
        public IActionResult GetAllBills()
        {
            var bills = _billRepository.GetAllBills();
            return Ok(bills);
        }
        #endregion

        #region Get Bill By ID
        [HttpGet("{id}")]
        public IActionResult GetBillById(int id)
        {
            var bill = _billRepository.SelectByPk(id);
            if (bill == null)
            {
                return NotFound("Bill not found");
            }
            return Ok(bill);
        }
        #endregion

        #region Insert Bill
        [HttpPost]
        public IActionResult InsertBill([FromBody] BillModel bill)
        {
            if (bill == null)
            {
                return BadRequest("Invalid bill data");
            }

            var isInserted = _billRepository.Insert(bill);
            if (isInserted)
            {
                return Ok(new { Message = "Bill inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the bill");
        }
        #endregion

        #region Update Bill
        [HttpPut("{id}")]
        public IActionResult UpdateBill(int id, [FromBody] BillModel bill)
        {
            if (bill == null || id != bill.BillID)
            {
                return BadRequest("Invalid bill data or ID mismatch");
            }

            var isUpdated = _billRepository.Update(bill);
            if (!isUpdated)
            {
                return NotFound("Bill not found");
            }

            return NoContent();
        }
        #endregion

        #region Delete Bill
        [HttpDelete("{id}")]
        public IActionResult DeleteBill(int id)
        {
            var isDeleted = _billRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Bill not found");
            }
            return NoContent();
        }
        #endregion

        #region Order DropDown
        [HttpGet("orders")]
        public IActionResult GetOrdrs()
        {
            var orders = _billRepository.GetOrder();
            if (!orders.Any())
            {
                return NotFound("No Orders found");
            }
            return Ok(orders);
        }
        #endregion

        #region User DropDown
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _billRepository.GetUser();
            if (!users.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(users);
        }
        #endregion
    }
}
