using Conexia.Challenge.Domain;

namespace Conexia.Challenge.Infra.Data.Factories
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork StartUnitOfWork(bool usingTransaction = false)
        {
            ((UnitOfWork)_unitOfWork).Start(usingTransaction);

            return _unitOfWork;
        }
    }
}
