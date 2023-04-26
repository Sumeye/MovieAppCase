using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using MovieAppCase.Core.PaginationFilter;

namespace MovieAppCase.Core.Services
{
    public interface IMoviesService : IService<Movies>
    {

        Task<Movies?> GetMovieBySourceIdAsync(int sourceId);
        Task<MovieAndMovieVoteDto> GetMovieAndMovieVoteBySourceId(int sourceId);
        Task<IEnumerable<Movies>> GetAllByPageSizeAsync(Pagination filter);
    }
}
