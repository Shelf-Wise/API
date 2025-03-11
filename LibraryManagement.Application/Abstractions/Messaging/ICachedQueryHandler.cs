namespace LibraryManagement.Application.Abstractions.Messaging
{
    public interface ICachedQueryHandler<TQuery, TResponse>
        where TQuery : ICachedQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
