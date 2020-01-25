using System;

namespace Conexia.Challenge.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
