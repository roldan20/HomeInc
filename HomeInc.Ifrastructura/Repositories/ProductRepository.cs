using HomeInc.Domain.DTOS;
using HomeInc.Domain.Entities;
using HomeInc.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HomeInc.Ifrastructura.Repositories
{
    public class ProductoRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetProductDTO>> GetAllAsync()
        {
            var products = await _context.products.ToListAsync();
            var users = await _context.users.ToListAsync();
            var result = users.Join(products,
                                   user => user.Id,
                                   product => product.UserId,
                                   (user, product) => new GetProductDTO
                                   {
                                    Id = product.Id,
                                    Name = product.Name,  
                                    DateOfCreate = product.DateOfCreate.ToString("dd-MM-yyyy"),
                                    Description = product.Description,
                                    Category = product.Category,
                                    TypeOfGuarantee = product.TypeOfGuarantee,
                                    UserId = user.Id,
                                    UserName = user.UserName,
                                   });
            return result;
        }

        public async Task<GetProductDTO> GetByIdAsync(Guid id)
        {
            var products = await _context.products.FindAsync(id);
            var user = await _context.users.FindAsync(products.UserId);

            var getProductDTO = new GetProductDTO
            {
                Id = products.Id,
                Description = products.Description,
                Name = products.Name,
                DateOfCreate = products.DateOfCreate.ToString("dd-MM-yyyy"),
                Category = products.Category,
                TypeOfGuarantee = products.TypeOfGuarantee,
                UserId = products.UserId,
                UserName = user.UserName
            };


            return getProductDTO;
        }

        public async Task AddAsync(CreateProductDTO productoDTO)
        {
            var producto = new Product
            {
                Name = productoDTO.Name,
                Category = productoDTO.Category,
                DateOfCreate = DateTime.Now,
                Description = productoDTO.Description,
                TypeOfGuarantee = productoDTO.TypeOfGuarantee,
                UserId = Guid.Parse(productoDTO.UserId.ToString()),
            };
            await _context.products.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateProductDTO producto)
        {
            var product = await _context.products.FindAsync(producto.Id);
            product.Name = producto.Name;
            product.Description = producto.Description;
            product.TypeOfGuarantee = producto.TypeOfGuarantee;
            product.Category = producto.Category;
            _context.products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var producto = await _context.products.FindAsync(id);
            if (producto != null)
            {
                _context.products.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }
    }
}
