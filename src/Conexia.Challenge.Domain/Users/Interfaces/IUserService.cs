using System.Threading.Tasks;

namespace Conexia.Challenge.Domain.Users.Interfaces
{
    public interface IUserService
    {
        Task<User> RecoverByIdAsync(int id);
        Task<User> LoginAsync(string email, string password);
    }
}