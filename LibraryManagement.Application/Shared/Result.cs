namespace LibraryManagement.Application.Shared
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
                throw new ArgumentException("Invalid error", nameof(error));

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public static Result Success() => new(true, Error.None);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Failure<TValue>(TValue? value, Error error) =>
            new(value, false, error);
    }
}



//these three classes is a design pattern that serves a similar purpose in terms of handling outcomes and errors in a clean, consistent manner.
//similar to a middleware
