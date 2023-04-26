using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.Services
{
    public interface IMovieVoteService : IService<MovieVotes>
    {
        Task AddMovieVote(MovieVoteDto movieVoteDto);
    }
}
