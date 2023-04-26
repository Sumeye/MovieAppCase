using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Core.Repositories
{
    public interface IMovieRepository:IGenericRepository<Movies>
    {
        Task<Movies> GetMovieBySourceIdAsync(int sourceId);
        Task<MovieAndMovieVoteDto> GetMovieAndMovieVoteBySourceId(int sourceId);
    }
}
