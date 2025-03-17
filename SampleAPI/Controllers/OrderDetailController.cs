using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Model;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailRepository _orderDetailRepository;

        public OrderDetailController(OrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        #region Get All Order Details
        [HttpGet]
        [Route("GetOrderDetails")]
        public IActionResult GetAllOrderDetails()
        {
            var orderDetails = _orderDetailRepository.GetAllOrderDetails();
            return Ok(orderDetails);
        }
        #endregion

        #region Get Order Detail By ID
        [HttpGet("{id}")]
        public IActionResult GetOrderDetailById(int id)
        {
            var orderDetail = _orderDetailRepository.SelectByPk(id);
            if (orderDetail == null)
            {
                return NotFound("Order detail not found");
            }
            return Ok(orderDetail);
        }
        #endregion

        #region Insert Order Detail
        [HttpPost]
        public IActionResult InsertOrderDetail([FromBody] OrderDetailModel orderDetail)
        {
            if (orderDetail == null)
            {
                return BadRequest("Invalid order detail data");
            }

            var isInserted = _orderDetailRepository.Insert(orderDetail);
            if (isInserted)
            {
                return Ok(new { Message = "Order detail inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the order detail");
        }
        #endregion

        #region Update Order Detail
        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(int id, [FromBody] OrderDetailModel orderDetail)
        {
            if (orderDetail == null || id != orderDetail.OrderDetailID)
            {
                return BadRequest("Invalid order detail data or ID mismatch");
            }

            var isUpdated = _orderDetailRepository.Update(orderDetail);
            if (!isUpdated)
            {
                return NotFound("Order detail not found");
            }

            return NoContent();
        }
        #endregion

        #region Delete Order Detail
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetail(int id)
        {
            var isDeleted = _orderDetailRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Order detail not found");
            }
            return NoContent();
        }
        #endregion

        #region Order DropDown
        [HttpGet("orders")]
        public IActionResult GetOrdrs()
        {
            var orders = _orderDetailRepository.GetOrder();
            if(!orders.Any())
            {
                return NotFound("No Orders found");
            }
            return Ok(orders);
        }
        #endregion

        #region Product DropDown
        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            var products = _orderDetailRepository.GetProduct();
            if(!products.Any())
            {
                return NotFound("No Products found");
            }
            return Ok(products);
        }
        #endregion

        #region User DropDown
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _orderDetailRepository.GetUser();
            if (!users.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(users);
        }
        #endregion
    }
}
