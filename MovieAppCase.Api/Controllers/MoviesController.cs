using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppCase.API.RabbitMQ;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.PaginationFilter;
using MovieAppCase.Core.Services;

namespace MovieApp.API.Controllers
{
    [Authorize]
    public class MoviesController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IMoviesService _moviesService;
        private readonly IRabbitMqProducer _rabitMQProducer;

        public MoviesController(IMoviesService moviesService,
            IMapper mapper,
            IRabbitMqProducer rabitMQProducer)
        {
            _moviesService = moviesService;
            _mapper = mapper;
            _rabitMQProducer = rabitMQProducer;

        }

        /// <summary>
        /// Film listesi
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllByPageSizeAsync")]
        public async Task<IActionResult> GetAllByPageSizeAsync([FromQuery] Pagination filter)
        {
            var movies = await _moviesService.GetAllByPageSizeAsync(filter);
            var movieDtos = _mapper.Map<List<MovieDto>>(movies.ToList());
            return CreateActionResult(CustomResponseDto<List<MovieDto>>.Success(200, movieDtos));
        }

        /// <summary>
        /// Id ile film görüntüleme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetMovieAndMovieVoteBySourceId/{id}")]
        public async Task<IActionResult> GetMovieAndMovieVoteBySourceId(int id)
        {
            var movies = await _moviesService.GetMovieAndMovieVoteBySourceId(id);
            var movieDtos = _mapper.Map<MovieAndMovieVoteDto>(movies);
            return CreateActionResult(CustomResponseDto<MovieAndMovieVoteDto>.Success(200, movieDtos));
        }

        [HttpPost("{id}/suggestion")]
        public IActionResult Queue(int id, SuggestionDto suggestionDto)
        {
            var movie = _moviesService.GetByIdAsync(id);
            
            suggestionDto.MovieTitle = movie.Result.Title;
            suggestionDto.MovieOverview = movie.Result.Overview;

            _rabitMQProducer.SendMessage(suggestionDto);

            return CreateActionResult(CustomResponseDto<MovieDto>.Success(200));
        }

        [HttpPost("job")]
        public async Task<IActionResult> Job()
        {
            return CreateActionResult(CustomResponseDto<MovieApiResultDto>.Success(200));
        }
    }
}
