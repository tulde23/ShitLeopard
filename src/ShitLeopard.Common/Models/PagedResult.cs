using System.Collections.Generic;

namespace ShitLeopard.Common.Models
{
    public class PagedResult<T>
    {
        public int Count { get; set; }
        public List<T> Result { get; set; }
    }
}