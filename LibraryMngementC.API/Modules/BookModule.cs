using LibraryManagement.Application.Features.Books.Commands;
using LibraryManagement.Application.Features.Books.Queries;
using LibraryMngementC.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMngementC.API.Modules
{
    public class BookModule : IApiModule
    {
        void IApiModule.MapEndEndpoint(WebApplication app)
        {
            var MapGroup = app.MapGroup("api/books")
                .WithTags("Book");

            MapGroup.MapPost(
                "/",
                async (CreateBookCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );

            MapGroup.MapPut(
                "/",
                async (UpdateBookCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );

            MapGroup.MapDelete(
                "/{id}",
                async (Guid id, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(new DeleteBookCommand { Id = id }));
                }
            );

            MapGroup.MapGet(
                "/",
                async ([FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(new GetAllBooksQuery()));
                }
            );

            MapGroup.MapGet(
                "/{id}",
                async (Guid id, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(new GetBookByIdQuery(id)));
                }
            );
        }
    }
}


//DeleteBookCommand { Id = id } is  OBJECT INITIALIZER
// Allows you to initialize properties of an object at the time of its creation without explicitly calling a constructor for each property.
