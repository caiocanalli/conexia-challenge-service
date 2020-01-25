using AutoMapper;
using Conexia.Challenge.Application.Documents.Models;
using Conexia.Challenge.Application.Documents.Responses;
using Conexia.Challenge.Domain.Documents;
using Conexia.Challenge.Domain.Models;

namespace Conexia.Challenge.Application.Documents.Mappers
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentModel>();
            CreateMap<PagedResult<Document>, FilterResponse>();
        }
    }
}
