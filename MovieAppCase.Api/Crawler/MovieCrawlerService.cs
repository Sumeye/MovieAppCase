using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using MovieAppCase.Core.Repositories;
using MovieAppCase.Core.UnitOfWork;
using Newtonsoft.Json;

namespace MovieAppCase.Api.Crawler
{
    public class MovieCrawlerService : IMovieCrawlerService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieCrawlerService(
            IMovieRepository movieRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteMovieCrawler()
        {
            var totalPage = 500; // Api rate limit. Page must be less than or equal to 500. 

            //var total = 0;

            for (int i = 1; i <= totalPage; i++)
            {
                var movieDtoList = fetchTheMovieDb(i).Result.results;
                //total += movieDtoList.Count;
                foreach (var movieDto in movieDtoList)
                {
                    var movie = await GetMovieBySourceIdAsync(movieDto.id);
                    if (movie == null)
                    {
                        try
                        {
                            await _movieRepository.AddAsync(_mapper.Map<Movies>(movieDto));
                            await _unitOfWork.CommitAsync();
                        }
                        catch (System.AggregateException ex)
                        {
                            //movieDtoList: will be logging
                            continue;
                        }
                    }
                    else
                    {
                        //await _moviesService.UpdateAsync(_mapper.Map<Movies>(movieDto));
                    }
                }
            }
        }

        private async Task<MovieApiDto> fetchTheMovieDb(int page)
        {
            string apiUrl = "https://api.themoviedb.org//3//movie//popular?api_key=7b03ee69bb87022df832cd1f2c135fec&page=" + page;

            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(apiUrl).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var movieJsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<MovieApiDto>(movieJsonString);
                    }

                    return null;
                }
            }
        }

        public async Task<Movies?> GetMovieBySourceIdAsync(int sourceId)
        {
            return await _movieRepository.Where(x => x.SourceId == sourceId).FirstOrDefaultAsync();
        }
    }
}
