using MediatR;

namespace LibraryManagement.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<TResponse> { }
}
