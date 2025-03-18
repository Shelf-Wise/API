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
                .ForMember(dest => dest.ImageUrl, options => options.MapFrom(src => src.ImageURL))
                .ForMember(dest => dest.Genres, options => options.MapFrom(src => src.Genres))
                .ForMember(
                    dest => dest.PublicationYear,
                    options => options.MapFrom(src => src.PublicationYear)
                );

            CreateMap<Book, GetAllBooksResultDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Author, options => options.MapFrom(src => src.Author))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.Status, options => options.MapFrom(src => src.Status))
                .ForMember(dest => dest.ImageURL, options => options.MapFrom(src => src.ImageURL))
                .ForMember(dest => dest.Genres, options => options.MapFrom(src => src.Genres))
                .ForMember(
                    dest => dest.PublicationYear,
                    options => options.MapFrom(src => src.PublicationYear)
                );

            CreateMap<GetBookResultDto, Book>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Author, options => options.MapFrom(src => src.Author))
                .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.ImageURL, options => options.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.ISBN, options => options.MapFrom(src => src.isbn))
                .ForMember(
                    dest => dest.PublicationYear,
                    options => options.MapFrom(src => src.PublicationYear)
                );

            CreateMap<BorrowHistory, GetBorrowedBooksByMemberIdResponse>()
              .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
              .ForMember(dest => dest.BookId, options => options.MapFrom(src => src.Book.Id))
              .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Book.Title))
              .ForMember(dest => dest.Author, options => options.MapFrom(src => src.Book.Author))
              .ForMember(dest => dest.BookStatus, options => options.MapFrom(src => src.Book.Status))
              .ForMember(dest => dest.DueDate, options => options.MapFrom(src => src.DueDate))
              .ForMember(dest => dest.BorrowDate, options => options.MapFrom(src => src.BorrowedDate))
              .ForMember(dest => dest.ReturnDate, options => options.MapFrom(src => src.ReturnDate))
              .ForMember(dest => dest.CreatedAt, options => options.MapFrom(src => src.CreatedAt))
              .ForMember(dest => dest.ImageUrl, options => options.MapFrom(src => src.Book.ImageURL))
              .ForMember(
                  dest => dest.MemberId,
                  options => options.MapFrom(src => src.MemberId)
              );

            //CreateMap<IReadOnlyCollection<Book>, List<GetBookResultDto>>();
        }
    }
}
