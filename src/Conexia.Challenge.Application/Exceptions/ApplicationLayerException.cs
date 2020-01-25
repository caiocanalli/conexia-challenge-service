using System;

namespace Conexia.Challenge.Application.Exceptions
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
