using System;

namespace Conexia.Challenge.Application.Infrastructure.Exceptions
{
    public class InvalidUserException : Exception
    {
        public InvalidUserException()
        {
        }

        public InvalidUserException(string message) : base(message)
        {
        }
    }
}