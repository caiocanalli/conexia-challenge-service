﻿using System;

namespace Conexia.Challenge.Domain.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
