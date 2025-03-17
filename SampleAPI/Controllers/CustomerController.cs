using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Model;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #region Get All Customers
        [HttpGet]
        [Route("GetCustomers")]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            return Ok(customers);
        }
        #endregion

        #region Get Customer By ID
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _customerRepository.SelectByPk(id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }
            return Ok(customer);
        }
        #endregion

        #region Insert Customer
        [HttpPost]
        public IActionResult InsertCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
            {
                return BadRequest("Invalid customer data");
            }

            var isInserted = _customerRepository.Insert(customer);
            if (isInserted)
            {
                return Ok(new { Message = "Customer inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the customer");
        }
        #endregion

        #region Update Customer
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] CustomerModel customer)
        {
            if (customer == null || id != customer.CustomerID)
            {
                return BadRequest("Invalid customer data or ID mismatch");
            }

            var isUpdated = _customerRepository.Update(customer);
            if (!isUpdated)
            {
                return NotFound("Customer not found");
            }

            return NoContent();
        }
        #endregion

        #region Delete Customer
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var isDeleted = _customerRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Customer not found");
            }
            return NoContent();
        }
        #endregion

        #region DropDown For User
        [HttpGet("users")]
        public IActionResult GetCustomers()
        {
            var customers = _customerRepository.GetCustomer();
            if (!customers.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(customers);
        }
        #endregion
    }
}
