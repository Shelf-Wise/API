namespace LibraryManagement.Application.Features.Genre.Dto
{
    public record GetGenreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
