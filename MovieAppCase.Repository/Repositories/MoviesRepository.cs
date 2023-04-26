using Microsoft.EntityFrameworkCore;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using MovieAppCase.Core.Repositories;
using MovieAppCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Repository.Repositories
{
    public class MoviesRepository : GenericRepository<Movies>, IMovieRepository
    {
        public MoviesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Movies> GetMovieBySourceIdAsync(int sourceId)
        {
            return await _context.Movies.Where(x => x.SourceId == sourceId).FirstAsync();
        }
        public async Task<MovieAndMovieVoteDto> GetMovieAndMovieVoteBySourceId(int sourceId)
        {
            var result = from movie in _context.Movies
                         join movieVote in _context.MovieVotes on movie.SourceId equals movieVote.SourceId
                         select new MovieAndMovieVoteDto
                         {
                             Overview = movie.Overview,
                             Title = movie.Title,
                             ReleaseDate = movie.ReleaseDate,
                             Note = movieVote.Note,
                             Vote = (int)(from v in _context.MovieVotes where v.SourceId.Equals(movie.SourceId) select v.Vote).Average(),
                             MovieVotes = _context.MovieVotes.Where(x=>x.SourceId==movie.SourceId).ToList(),
                         };

            if (result.Any())
            {
                return await result.FirstAsync();
            }
            else
            {
                return null;
            }
        }
    }
}

