using LibraryManagement.Application.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LibraryManagement.Application.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            Log.Information(
                "Request Begins {@RequestName}, {@DateTimeUTc}",
                typeof(TRequest).Name,
                DateTime.UtcNow
            );

            var result = await next();

            if (result.IsFailure)
            {
                var failure = result as Result;

                Log.Error(
                    "REQUEST *********************************************************** NAME {@RequestName} failed: {@ErrorMessage}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    failure.Error,
                    DateTime.UtcNow
                );
            }
            Log.Information(
                "Completed ************************************************  Request {@RequestName},{@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow
            );

            return result;
        }
    }
}
