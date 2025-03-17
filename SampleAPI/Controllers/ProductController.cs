using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Model;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #region Get All Products
        [HttpGet]
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetProducts();
            return Ok(products);
        }
        #endregion

        #region Get Product By ID
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.SelectByPk(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        #endregion

        #region Insert Product
        [HttpPost]
        public IActionResult InsertProduct([FromBody] ProductModel product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data");
            }

            var isInserted = _productRepository.Insert(product);
            if (isInserted)
            {
                return Ok(new { Message = "Product inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the product");
        }
        #endregion

        #region Update Product
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductModel product)
        {
            if (product == null || id != product.ProductID)
            {
                return BadRequest("Invalid product data or ID mismatch");
            }

            var isUpdated = _productRepository.Update(product);
            if (!isUpdated)
            {
                return NotFound("Product not found");
            }

            return NoContent();
        }
        #endregion

        #region Delete Product
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var isDeleted = _productRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Product not found");
            }
            return NoContent();
        }
        #endregion

        #region User Dropdown
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _productRepository.GetUser();
            if(!users.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(users);
        }
        #endregion
    }
}
