using Microsoft.AspNetCore.Http;

namespace Conexia.Challenge.Application.Documents.Requests
{
    public class UploadRequest
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
