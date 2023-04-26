using Microsoft.EntityFrameworkCore;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using MovieAppCase.Core.PaginationFilter;
using MovieAppCase.Core.Repositories;
using MovieAppCase.Core.Services;
using MovieAppCase.Core.UnitOfWork;

namespace MovieAppCase.Service.Services
{
    public class MoviesService : Service<Movies>, IMoviesService
    {
        private readonly IMovieRepository _movieRepository;
        public MoviesService(IGenericRepository<Movies> repository,
            IUnitOfWork unitOfWork,
            IMovieRepository movieRepository
            ) : base(repository, unitOfWork)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IEnumerable<Movies>> GetAllByPageSizeAsync(Pagination filter)
        {
            return await _movieRepository.GetAll()
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
        }

        public async Task<Movies?> GetMovieBySourceIdAsync(int sourceId)
        {
            return await _movieRepository.Where(x => x.SourceId == sourceId).FirstOrDefaultAsync();
        }
        public async Task<MovieAndMovieVoteDto> GetMovieAndMovieVoteBySourceId(int sourceId)
        {
            return await _movieRepository.GetMovieAndMovieVoteBySourceId(sourceId);
        }
    }
}
