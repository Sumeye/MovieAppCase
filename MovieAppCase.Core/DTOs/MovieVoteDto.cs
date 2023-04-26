using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.DTOs
{
    public class MovieVoteDto
    {
        public int Vote { get; set; }
        public string Note { get; set; }
        public int SourceId { get; set; }
    }
}
