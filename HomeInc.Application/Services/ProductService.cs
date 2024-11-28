using HomeInc.Domain.DTOS;
using HomeInc.Domain.Entities;
using HomeInc.Domain.Repositories;


namespace HomeInc.Application.Services
{
    public class ProductService 
    {
        private readonly IProductRepository _ProductRepository;
        public ProductService(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        } 


        public async Task CreateAsync(ProductDTO product)
        {
           await _ProductRepository.AddAsync(product);
        }

        public async Task<bool> DeleteProductAsync(Guid Id)
        {
            await _ProductRepository.DeleteAsync(Id);
            return true;
        }

        public Task<IEnumerable<GetProductDTO>> GetAllProducts()
        {
            return _ProductRepository.GetAllAsync();
        }

        public async Task<GetProductDTO> GetProductById(Guid Id)
        {
          return  await _ProductRepository.GetByIdAsync(Id);
        }

        public async Task UpdateProductAsync(Product product)
        {
   
           await _ProductRepository.UpdateAsync(product);

        }
    }
}
