using Conexia.Challenge.Domain.Users.Interfaces;
using System.Threading.Tasks;

namespace Conexia.Challenge.Domain.Users
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(string email, string password) =>
            await _userRepository.LoginAsync(email, password);

        public async Task<User> RecoverByIdAsync(int id) =>
            await _userRepository.RecoverByIdAsync(id);
        }
}