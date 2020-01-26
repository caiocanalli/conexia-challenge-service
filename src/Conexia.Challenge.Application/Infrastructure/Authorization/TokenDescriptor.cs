namespace Conexia.Challenge.Application.Infrastructure.Authorization
{
    public class TokenDescriptor
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int MinutesValid { get; set; }
    }
}
