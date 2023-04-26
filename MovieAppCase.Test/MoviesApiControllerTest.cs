using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApp.API.Controllers;
using MovieApp.Service.Mapping;
using MovieAppCase.Api.Controllers;
using MovieAppCase.API.RabbitMQ;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;
using MovieAppCase.Core.PaginationFilter;
using MovieAppCase.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppCase.Test
{

    public class MoviesApiControllerTest
    {
        private readonly Mock<IMoviesService> _moviesService;
        private readonly MoviesController _moviesController;
        private readonly IMapper _mapper;
        private readonly IRabbitMqProducer _rabitMQProducer;
        private List<Movies> movies;
        public MoviesApiControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;

            }
            _moviesService = new Mock<IMoviesService>();
            IRabbitMqProducer rabitMQProducer;
            _moviesController = new MoviesController(_moviesService.Object, _mapper, _rabitMQProducer);

            movies = new List<Movies>() {
             new Movies
            {
                 SourceId=640146,
                 Overview="Super-Hero partners Scott Lang and Hope van Dyne, along with with Hope's parents Janet van Dyne and Hank Pym",
                 Title="Ant-Man and the Wasp: Quantumania",
                 PosterPath="/ngl2FKBlU4fhbdsrtdom9LVLBXw.jpg",
                 ReleaseDate= DateTime.Parse("2023-02-15T00:00:00")
            },
                  new Movies
            {
                 SourceId=502356,
                 Overview="Super-Hero partners Scott Lang and Hope van Dyne, along with with Hope's parents Janet van Dyne and Hank Pym",
                 Title="Ant-Man and the Wasp: Quantumania",
                 PosterPath="/ngl2FKBlU4fhbdsrtdom9LVLBXw.jpg",
                 ReleaseDate= DateTime.Parse("2023-02-15T00:00:00")
            },
            };
        }
        [Fact]
        public async void GetAllByPageSizeAsync_ActionExecutes_ReturnObjectResultMovieList()
        {
            var filter = new Pagination { PageNumber = 1, PageSize = 2 };
            _moviesService.Setup(x => x.GetAllByPageSizeAsync(filter)).ReturnsAsync(movies);
            var result = await _moviesController.GetAllByPageSizeAsync(filter);
            var okResult = Assert.IsType<ObjectResult>(result);
            var returnUsers = Assert.IsAssignableFrom<CustomResponseDto<List<MovieDto>>>(okResult.Value);
            Assert.Equal<int>(2, returnUsers.Data.ToList().Count);
        }


    }

}
