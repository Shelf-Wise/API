using AutoMapper;
using LibraryManagement.Application.Features.Books.Dtos;
using LibraryManagementC.Domain.Entities;

namespace LibraryManagement.Application.Profiles
{
    internal class BookProfiles : Profile
    {
        public BookProfiles()
        {
            CreateMap<Book, GetBookResultDto>()
                .ForMember(dest => dest.BookId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Author, options => options.MapFrom(src => src.Author))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.IsAvailable, options => options.MapFrom(src => src.Status))
                .ForMember(
                    dest => dest.PublicationYear,
                    options => options.MapFrom(src => src.PublicationYear)
                );

            CreateMap<Book, GetAllBooksResultDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Author, options => options.MapFrom(src => src.Author))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Status, options => options.MapFrom(src => src.Status))
                .ForMember(
                    dest => dest.PublicationYear,
                    options => options.MapFrom(src => src.PublicationYear)
                );

            CreateMap<GetBookResultDto, Book>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Author, options => options.MapFrom(src => src.Author))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(
                    dest => dest.PublicationYear,
                    options => options.MapFrom(src => src.PublicationYear)
                );

            //CreateMap<IReadOnlyCollection<Book>, List<GetBookResultDto>>();
        }
    }
}
