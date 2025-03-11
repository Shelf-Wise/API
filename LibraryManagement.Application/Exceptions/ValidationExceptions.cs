namespace LibraryManagement.Application.Exceptions
{
    public class ValidationExceptions : Exception
    {
        public IReadOnlyCollection<ValidationError> _errors { get; set; }

        public ValidationExceptions(IReadOnlyCollection<ValidationError> errors)
        {
            _errors = errors;
        }

        public override string ToString()
        {
            var errorMessages = _errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}");
            return $"Validation failed: {string.Join(", ", errorMessages)}";
        }

        public record ValidationError(string PropertyName, string ErrorMessage);
    }
}
