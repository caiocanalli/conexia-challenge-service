using System.ComponentModel.DataAnnotations;

namespace Conexia.Challenge.Application.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        public string Password { get; set; }
    }
}