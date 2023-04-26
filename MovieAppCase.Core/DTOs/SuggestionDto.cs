using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieAppCase.Core.DTOs
{
    public class SuggestionDto
    {
        [JsonIgnore]
        public string? MovieTitle { get; set; }

        [JsonIgnore]
        public string? MovieOverview { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}
