using AutoMapper;
using LibraryManagement.Application.Features.Genre.Dto;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Profiles
{
    internal class GenreProfiles : Profile
    {
        public GenreProfiles()
        {
            CreateMap<Genre, GetGenreDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
