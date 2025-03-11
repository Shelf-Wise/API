using LibraryManagement.Application.Features.LibraryMembers.Commands;
using LibraryMngementC.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LibraryMngementC.API.Modules
{
    public class ServicesModule : IApiModule
    {
        public void MapEndEndpoint(WebApplication app)
        {
            var MapGroup = app.MapGroup("api/services")
                .WithTags("Services");
//
            // .RequireAuthorization("AdminPriviledges");

            MapGroup.MapPost(
                "/BorrowBook",
                async (BorrowBookCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );

            MapGroup.MapPost(
                "/ReturnBook",
                async (ReturnBookCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );
        }
    }
}
