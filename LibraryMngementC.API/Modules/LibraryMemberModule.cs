using LibraryManagement.Application.Features.LibraryMembers.Query;
using LibraryMngementC.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMngementC.API.Modules
{
    public class LibraryMemberModule : IApiModule
    {
        public void MapEndEndpoint(WebApplication app)
        {
            var MapGroup = app.MapGroup("LibraryMembers")
                .WithTags("LibraryMembers")
                .RequireAuthorization("LowLevelPriviledges");

            MapGroup.MapGet(
                "/",
                async ([FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(new GetAllLibraryMembersQuery()));
                }
            );

            MapGroup.MapGet(
                "/{id}",
                async (Guid id, [FromServices] IMediator _mediator) =>
                {
                    return Results.Ok(await _mediator.Send(new GetLibraryMemberQuery(id)));
                }
            );
        }
    }
}
