using Conexia.Challenge.Application.Models;
using System;

namespace Conexia.Challenge.Application.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
        public UserModel User { get; set; }
    }
}