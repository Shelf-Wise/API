using LibraryManagement.Application.Features.Genre.Command;
using LibraryManagement.Application.Features.Genre.Query;
using LibraryMngementC.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMngementC.API.Modules
{
    public class GenreModule : IApiModule
    {
        void IApiModule.MapEndEndpoint(WebApplication app)
        {
            var MapGroup = app.MapGroup("api/genres")
                .WithTags("Genre");

            MapGroup.MapPost(
                "/",
                async (CreateGenreCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );

            MapGroup.MapGet(
                "/",
                async ([FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(new GetAllGenresQuery()));
                }
            );
        }
    }
}
