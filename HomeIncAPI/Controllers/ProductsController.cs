using HomeInc.Domain.DTOS;
using HomeInc.Application.Services;
using HomeInc.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HomeInc.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productoService;

        public ProductsController(ProductService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var productos = await _productoService.GetAllProducts();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            var producto = await _productoService.GetProductById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductCreateDTO product)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var productDTO = new ProductDTO
            {
                Description = product.Description,
                Category = product.Category,
                Name = product.Name,
                TypeOfGuarantee = product.TypeOfGuarantee,
                UserId = Guid.Parse(userId.ToString())
            }; 

            await _productoService.CreateAsync(productDTO);
            return CreatedAtAction(nameof(Get), new { id = productDTO.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, Product producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }
            await _productoService.UpdateProductAsync(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _productoService.DeleteProductAsync(id);
            return NoContent();
        }
    }
    }
