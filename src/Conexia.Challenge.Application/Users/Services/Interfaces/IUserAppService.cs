using Conexia.Challenge.Application.Responses;
using System.Threading.Tasks;

namespace Conexia.Challenge.Application.Users.Services.Interfaces
{
    public interface IUserAppService
    {
        Task<RecoverByIdResponse> RecoverByIdAsync(int id);
        Task<LoginResponse> LoginAsync(string email, string password);
    }
}
