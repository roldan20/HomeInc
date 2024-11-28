using HomeInc.Domain.DTOS;
using HomeInc.Domain.Entities;

namespace HomeInc.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<GetProductDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<GetProductDTO>> GetAllAsync();
        Task AddAsync(CreateProductDTO product);
        Task UpdateAsync(UpdateProductDTO product);
        Task DeleteAsync(Guid id);
    }
}
