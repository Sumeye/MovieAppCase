using MovieAppCase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.DTOs
{
    public class MovieAndMovieVoteDto
    {
        public int SourceId { get; set; }
        public string? Overview { get; set; }
        public string? Title { get; set; }
        public string? PosterPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Vote { get; set; }
        public string? Note { get; set; }
        public List<MovieVotes>? MovieVotes { get; set; }
    }
}
