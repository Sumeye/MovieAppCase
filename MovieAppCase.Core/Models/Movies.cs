using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.Models
{
    public class Movies
    {
        public int MovieId { get; set; }
        /// <summary>
        /// Apiden gelen movieId
        /// </summary>
        public int SourceId { get; set; }
        public string? Overview { get; set; }
        public string? Title { get; set; }
        public string? PosterPath { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}
