using AutoMapper;
using Movie_Web.Models;
using Movie_Web.Models.ApiResponse;
using Movie_Web.Models.DTOs;

namespace Movie_Web.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MovieDetailDTO, Movie>();
            CreateMap<MovieDurationDTO, Movie>();
            CreateMap<LoginApiResponse, User>();
            CreateMap<RegisterApiResponse, User>();
        }
    }
}
