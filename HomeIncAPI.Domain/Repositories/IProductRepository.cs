using HomeInc.Domain.DTOS;
using HomeInc.Domain.Entities;

namespace HomeInc.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<GetProductDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<GetProductDTO>> GetAllAsync();
        Task AddAsync(ProductDTO product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
