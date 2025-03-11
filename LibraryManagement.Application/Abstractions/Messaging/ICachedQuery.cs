namespace LibraryManagement.Application.Abstractions.Messaging
{
    public interface ICachedQuery
    {
        public string Key { get; }
        public TimeSpan? Expiration { get; }
    }

    public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;
}
