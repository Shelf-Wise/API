using LibraryManagement.Application.Features.Members.Commands;
using LibraryManagement.Application.Features.Members.Queries;
using LibraryMngementC.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMngementC.API.Modules
{
    public class MemberModule : IApiModule
    {
        public void MapEndEndpoint(WebApplication app)
        {
            var MapGroup = app.MapGroup("/api/members")
                .WithTags("Members");
            MapGroup.MapPost(
                "/",
                async (CreateMemberCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );

            MapGroup.MapPut(
                "/",
                async (UpdateMemberCommand command, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(command));
                }
            );

            MapGroup.MapDelete(
                "/{id}",
                async (Guid id, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(
                        await _mediator.Send(new DeleteMemberCommand { MemberId = id })
                    );
                }
            );

            MapGroup.MapGet(
                "/",
                async ([FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(new GetAllMemberQuery()));
                }
            );

            MapGroup.MapGet(
                "/{id}",
                async (Guid id, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(new GetMemberByIdQuery(id)));
                }
            );
        }
    }

    public sealed record MemberRequest
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required string Email { get; init; }
        public required string Telephone { get; init; }
        public required string DOB { get; init; }
        public required string NIC { get; init; }
        public required string Address { get; init; }
    }
}
