﻿namespace LibraryManagement.Application.Features.LibraryMembers.Dtos
{
    public class GetLibraryMemberDto
    {
        public Guid MemberId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int NoOfBooksBorrowed { get; set; }
    }
}
