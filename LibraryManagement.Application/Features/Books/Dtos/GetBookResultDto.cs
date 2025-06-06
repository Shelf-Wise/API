﻿using LibraryManagementC.Domain.Enums;

namespace LibraryManagement.Application.Features.Books.Dtos
{
    public class GetBookResultDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public BookStatus IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public string isbn { get; set; }
        public Guid MemberId { get; set; }
        public IEnumerable<LibraryManagementC.Domain.Entities.Genre> Genres { get; set; } = new List<LibraryManagementC.Domain.Entities.Genre>();

    }
}
