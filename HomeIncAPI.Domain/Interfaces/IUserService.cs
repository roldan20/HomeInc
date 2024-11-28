
namespace HomeInc.Domain.Interfaces
{
    public interface IUserService
    {
        Task<string> Authenticate(string userName, string password);
    }
}
