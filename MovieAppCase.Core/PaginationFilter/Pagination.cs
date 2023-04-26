using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieAppCase.Core.PaginationFilter
{
    public class Pagination
    {
        [JsonIgnore]
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Pagination()
        {
            this.PageNumber = 1;
            this.PageSize = 20;
        }
        public Pagination(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 20 ? 20 : pageSize;
        }
    }
}
