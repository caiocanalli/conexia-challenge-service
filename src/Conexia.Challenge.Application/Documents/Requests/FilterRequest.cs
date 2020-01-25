using System;

namespace Conexia.Challenge.Application.Documents.Requests
{
    public class FilterRequest
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
