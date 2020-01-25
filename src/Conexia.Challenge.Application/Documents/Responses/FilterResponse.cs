using Conexia.Challenge.Application.Documents.Models;
using System.Collections.Generic;

namespace Conexia.Challenge.Application.Documents.Responses
{
    public class FilterResponse
    {
        public List<DocumentModel> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int TotalSize { get; set; }
    }
}
