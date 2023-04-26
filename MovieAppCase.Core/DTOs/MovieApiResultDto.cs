using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.DTOs
{
    public class MovieApiResultDto
    {
        public int id { get; set; }
        public string? overview { get; set; }
        public string? title { get; set; }

        public string? poster_path { get; set; }

        public DateTime? release_date { get; set; }
    }
}
