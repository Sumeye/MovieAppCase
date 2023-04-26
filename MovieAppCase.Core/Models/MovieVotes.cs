using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.Models
{
    public class MovieVotes
    {
        public int MovieVoteId  { get; set; }
        public int Vote { get; set;}
        public string Note { get; set;}
        public int SourceId { get; set; }

    }
}
