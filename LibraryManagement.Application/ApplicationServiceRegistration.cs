using System.Reflection;
using FluentValidation;
using LibraryManagement.Application.Behaviours;
using LibraryManagement.Application.Features.Authentication.Command;
using LibraryManagement.Application.Features.Authentication.Validators;
using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Application.Features.Books.Validators;
using LibraryManagement.Application.Features.LibraryMembers.Commands;
using LibraryManagement.Application.Features.LibraryMembers.Validators;
using LibraryManagement.Application.Features.Members.Commands;
using LibraryManagement.Application.Features.Members.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LibraryManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationService(
            this IServiceCollection services
        )
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(CachingBehaviour<,>));
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<IValidator<CreateBookCommand>, CreateBookCommandValidator>();
            services.AddScoped<IValidator<UpdateBookCommand>, UpdateBookCommandValidator>();
            services.AddScoped<IValidator<CreateMemberCommand>, CreateMemberCommandValidator>();
            services.AddScoped<IValidator<UpdateMemberCommand>, UpdateMemberCommandValidator>();
            services.AddScoped<IValidator<DeleteMemberCommand>, DeleteMemberCommandValidator>();
            services.AddScoped<IValidator<BorrowBookCommand>, BorrowBookCommandValidator>();
            services.AddScoped<IValidator<ReturnBookCommand>, ReturnBookCommandValidator>();

            services.AddScoped<IValidator<SignUpCommand>, SignUpCommandValidator>();
            services.AddScoped<IValidator<SignInCommand>, SignInCommandValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

            services.AddLogging(b =>
            {
                var logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();

                b.AddSerilog(logger);
            });

            return services;
        }
    }
}
