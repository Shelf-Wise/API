﻿using LibraryManagement.Application.Abstractions.Services;
using LibraryManagement.Application.Features.Books.Queries;
using LibraryManagement.Application.Features.LibraryMembers.Commands;
using LibraryMngementC.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

            MapGroup.MapGet(
                "/getBorrowedBooksByMemberId/{memberId}",
                async (Guid memberId, [FromServices] IMediator mediator) =>
                {
                    var query = new GetBorrowedBooksByMemberIdQuery { MemberId = memberId };
                    var result = await mediator.Send(query);

                    if (result.IsSuccess)
                    {
                        return Results.Ok(result.Value);
                    }

                    return Results.BadRequest(result.Error);
                }
            );

            MapGroup.MapPost(
                "/upload-image",
                async (IFormFile file, [FromServices] ICloudfareServices _cloudfareServices) =>
                {
                    return Results.Ok(await _cloudfareServices.UploadFileAsync(file.OpenReadStream(), file.FileName, file.ContentType));
                }
            ).DisableAntiforgery();
        }
    }
}
