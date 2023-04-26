using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.DTOs
{
    public class MovieApiDto
    {
        public int page { get; set; }
        public List<MovieApiResultDto> results { get; set; }
        public int totalPages { get; set; }
        public int totalResults { get; set; }
    }
}
