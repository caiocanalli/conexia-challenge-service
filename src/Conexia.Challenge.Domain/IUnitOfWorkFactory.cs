namespace Conexia.Challenge.Domain
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork StartUnitOfWork(bool usingTransaction = false);
    }
}
