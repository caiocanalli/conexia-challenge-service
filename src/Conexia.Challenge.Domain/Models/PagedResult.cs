using System.Collections.Generic;

namespace Conexia.Challenge.Domain.Models
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Data { get; set; } = new List<T>();
    }
}
