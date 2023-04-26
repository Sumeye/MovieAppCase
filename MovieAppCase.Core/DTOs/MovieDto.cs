using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace MovieAppCase.Core.DTOs
{
    public class MovieDto
    {
        public int SourceId { get; set; }
        public string? Overview { get; set; }
        public string? Title { get; set; }

        public string? PosterPath { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
