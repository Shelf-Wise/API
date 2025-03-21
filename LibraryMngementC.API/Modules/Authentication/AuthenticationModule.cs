using LibraryManagement.Application.Features.Authentication.Command;
using LibraryMngementC.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMngementC.API.Modules.Authentication
{
    public class AuthenticationModule : IApiModule
    {
        public void MapEndEndpoint(WebApplication app)
        {
            var MapGroup = app.MapGroup("api")
                .WithTags("authentication")
                .WithDescription(
                    "There are only two roles as LibraryStaffMinor, LibraryStaffManagement"
                );

            MapGroup.MapPost(
                "/signUp",
                async (SignUpCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );

            MapGroup.MapPost(
                "/signIn",
                async (SignInCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );
        }
    }
}
