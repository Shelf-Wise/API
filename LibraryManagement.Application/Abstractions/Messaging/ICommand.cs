using LibraryManagement.Application.Shared;
using MediatR;

namespace LibraryManagement.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result> { }

    public interface ICommand<TResposne> : IRequest<Result<TResposne>> { }
}
