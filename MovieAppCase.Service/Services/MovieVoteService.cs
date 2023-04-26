using AutoMapper;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using MovieAppCase.Core.Repositories;
using MovieAppCase.Core.Services;
using MovieAppCase.Core.UnitOfWork;

namespace MovieAppCase.Service.Services
{

    public class MovieVoteService : Service<MovieVotes>, IMovieVoteService
    {
        private readonly IGenericRepository<MovieVotes> _movieVoteRepository;
        private readonly IGenericRepository<Movies> _moviesRepository;
        private readonly IUnitOfWork _unitOfWork;
        public MovieVoteService(IGenericRepository<MovieVotes> movieVoteRepository,
           IGenericRepository<Movies> movieRepository, IUnitOfWork unitOfWork,
           IMapper mapper) : base(movieVoteRepository, unitOfWork)
        {
            _movieVoteRepository = movieVoteRepository;
            _moviesRepository = movieRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddMovieVote(MovieVoteDto movieVoteDto)
        {
            var movie = _moviesRepository.Where(x => x.SourceId == movieVoteDto.SourceId);

            if (movie != null)
            {
                MovieVotes movieScore = new MovieVotes
                {
                    SourceId = movieVoteDto.SourceId,
                    Note = $"{movieVoteDto.Note}",
                    Vote = movieVoteDto.Vote,
                };
                await _movieVoteRepository.AddAsync(movieScore);
                await _unitOfWork.CommitAsync();
            }
            else
                throw new Exception("Böyle bir film bulunmamaktadır.");


        }
    }


}
