using Conexia.Challenge.Domain;
using NHibernate;

namespace Conexia.Challenge.Infra.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        ITransaction _transaction;

        public ISession Current { get; }

        public UnitOfWork(ISession session)
        {
            Current = session;
        }

        public void Start(bool usingTransaction)
        {
            if (usingTransaction)
            {
                InitializeTransaction();
            }
        }

        private void InitializeTransaction()
        {
            _transaction = Current.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            Current.Dispose();
        }
    }
}
