using MovieAppCase.Core.Models;
using MovieAppCase.Core.Repositories;
using MovieAppCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Repository.Repositories
{
    public class MovieVoteRepository : GenericRepository<MovieVotes>, IMovieVoteRepository
    {
        public MovieVoteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
