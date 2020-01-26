using Conexia.Challenge.Domain;
using Conexia.Challenge.Domain.Users;
using Conexia.Challenge.Domain.Users.Interfaces;
using NHibernate.Linq;
using System.Threading.Tasks;

namespace Conexia.Challenge.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly UnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        public async Task<User> RecoverByIdAsync(int id) =>
            await _unitOfWork.Current.Query<User>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> LoginAsync(string email, string password) =>
           await _unitOfWork.Current.Query<User>()
               .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }
}