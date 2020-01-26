using System;

namespace Conexia.Challenge.Application.Infrastructure.Exceptions
{
    public class ApplicationLayerException : Exception
    {
        public ApplicationLayerException()
        {
        }

        public ApplicationLayerException(string message) : base(message)
        {
        }
    }
}
