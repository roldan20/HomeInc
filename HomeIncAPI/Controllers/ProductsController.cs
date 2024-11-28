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
        public async Task<ActionResult<IEnumerable<GetProductDTO>>> Get()
        {
            var productos = await _productoService.GetAllProducts();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductDTO>> Get(Guid id)
        {
            var producto = await _productoService.GetProductById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateProductDTO createProductDTO)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            createProductDTO.UserId = Guid.Parse(userId);

            await _productoService.CreateAsync(createProductDTO);
            return CreatedAtAction(nameof(Get), new { id = createProductDTO.UserId }, createProductDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, UpdateProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }
  
            await _productoService.UpdateProductAsync(productDTO);
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
