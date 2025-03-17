using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Model;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #region Get All Orders
        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            return Ok(orders);
        }
        #endregion

        #region Get Order By ID
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderRepository.SelectByPk(id);
            if (order == null)
            {
                return NotFound("Order not found");
            }
            return Ok(order);
        }
        #endregion

        #region Insert Order
        [HttpPost]
        public IActionResult InsertOrder([FromBody] OrderModel order)
        {
            if (order == null)
            {
                return BadRequest("Invalid order data");
            }

            var isInserted = _orderRepository.Insert(order);
            if (isInserted)
            {
                return Ok(new { Message = "Order inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the order");
        }
        #endregion

        #region Update Order
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderModel order)
        {
            if (order == null || id != order.OrderID)
            {
                return BadRequest("Invalid order data or ID mismatch");
            }

            var isUpdated = _orderRepository.Update(order);
            if (!isUpdated)
            {
                return NotFound("Order not found");
            }

            return NoContent();
        }
        #endregion

        #region Delete Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var isDeleted = _orderRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Order not found");
            }
            return NoContent();
        }
        #endregion

        #region Customer DropDwon
        [HttpGet("customers")]
        public IActionResult GetCustomers()
        {
            var customers = _orderRepository.GetCustomer();
            if(!customers.Any())
            {
                return NotFound("No Customers Found");
            }
            return Ok(customers);
        }
        #endregion

        #region User DropDown
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _orderRepository.GetUser();
            if (!users.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(users);
        }
        #endregion
    }
}
