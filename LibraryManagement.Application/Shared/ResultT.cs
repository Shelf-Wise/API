namespace LibraryManagement.Application.Shared
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        protected internal Result(TValue? Value, bool isSuccess, Error error)
            : base(isSuccess, error) => _value = Value;

        public TValue? Value => IsSuccess ? _value! : default;
    }
}
