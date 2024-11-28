using HomeInc.Domain.DTOS;
using HomeInc.Domain.Entities;
using HomeInc.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


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
                Category = products.Category,
                TypeOfGuarantee = products.TypeOfGuarantee,
                UserId = products.UserId,
                UserName = user.UserName
            };


            return getProductDTO;
        }

        public async Task AddAsync(ProductDTO productoDTO)
        {
            var producto = new Product
            {
                Name = productoDTO.Name,
                Category = productoDTO.Category,
                DateOfCreate = DateTime.Now,
                Description = productoDTO.Description,
                TypeOfGuarantee = productoDTO.TypeOfGuarantee,
                UserId = productoDTO.UserId,
            };
            await _context.products.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product producto)
        {
            _context.products.Update(producto);
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
