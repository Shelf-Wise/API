using MediatR;

namespace LibraryManagement.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TReponse> : IRequestHandler<TQuery, TReponse>
        where TQuery : IQuery<TReponse>
    {
        new Task<TReponse> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
