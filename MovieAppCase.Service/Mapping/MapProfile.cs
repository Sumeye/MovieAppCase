using AutoMapper;
using MovieAppCase.Core.DTOs;
using MovieAppCase.Core.Models;

namespace MovieApp.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            CreateMap<UserAppDto, UserApp>().ReverseMap();

            CreateMap<Movies, MovieDto>().ReverseMap()
                .ForMember(m => m.SourceId, opt => opt.MapFrom(src => src.SourceId));

            CreateMap<Movies, MovieApiResultDto>().ReverseMap()
                .ForMember(m => m.SourceId, opt => opt.MapFrom(src => src.id))
                .ForMember(m => m.Overview, opt => opt.MapFrom(src => src.overview))
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(m => m.PosterPath, opt => opt.MapFrom(src => src.poster_path))
                .ForMember(m => m.ReleaseDate, opt => opt.MapFrom(src => src.release_date));
        }
    }
}
