using LibraryManagement.Application.Shared;
using MediatR;

namespace LibraryManagement.Application.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
        new Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
    }

    public interface IComandHandler<TCommand, TResponse>
        : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
        new Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
