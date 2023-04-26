using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using MovieAppCase.Core.Services;

namespace MovieApp.API.Controllers
{
    [Authorize]
    public class MovieVoteController : CustomBaseController
    {
        private readonly IMovieVoteService _movieScoreService;
        public MovieVoteController(IMovieVoteService movieScoreService )
        {
            _movieScoreService = movieScoreService;
        }
        /// <summary>
        /// Seçilen bir filme not ve puan ekleme
        /// </summary>
        /// <param name="movieVoteDto"></param>
        /// <returns></returns>
        [HttpPost("AddMovieVote")]
        public async Task<IActionResult>  AddMovieVote(MovieVoteDto movieVoteDto)
        {
            await _movieScoreService.AddMovieVote(movieVoteDto);
            return CreateActionResult(CustomResponseDto<MovieVotes>.Success(200));
        }
    }
}
