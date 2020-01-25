using Conexia.Challenge.Application.Documents.Requests;
using Conexia.Challenge.Application.Documents.Responses;
using System.Threading.Tasks;

namespace Conexia.Challenge.Application.Documents.Services.Interfaces
{
    public interface IDocumentAppService
    {
        Task UploadAsync(UploadRequest request);
        Task<FilterResponse> FilterAsync(FilterRequest request);
        Task UpdateAsync(UpdateRequest request);
        Task EvaluateAsync(int id);
    }
}
