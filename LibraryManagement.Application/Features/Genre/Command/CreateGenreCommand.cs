using LibraryManagement.Application.Abstractions.Messaging;

namespace LibraryManagement.Application.Features.Genre.Command
{
    public record CreateGenreCommand : ICommand
    {
        public string Name { get; set; }
    }
}
