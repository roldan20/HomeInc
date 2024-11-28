using HomeInc.Domain.Entities;
using HomeInc.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace HomeInc.Ifrastructura.Data
{
    public class UserRepository :  IUserRepository
    {
        private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByUsernameAsync(string userName)
    {
        return await _context.users.FirstOrDefaultAsync(x => x.UserName == userName);
    }
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.users.FindAsync(userId);
    }
    public async Task AddUserAsync(User user)
    {
        await _context.users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
}
