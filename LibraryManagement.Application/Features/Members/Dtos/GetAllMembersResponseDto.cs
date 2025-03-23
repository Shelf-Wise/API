namespace LibraryManagement.Application.Features.Members.Dtos
{
    public class GetAllMembersResponseDto
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string  Email { get; set; }
        public int NoOfBooksBorrowed { get; set; }
        public string ImageURL { get; set; }
    }
}
