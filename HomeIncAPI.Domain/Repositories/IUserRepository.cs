using HomeInc.Domain.Entities;

namespace HomeInc.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string userName);
        Task<User> GetUserByIdAsync(int userId);
        Task AddUserAsync(User user);
    }
}
