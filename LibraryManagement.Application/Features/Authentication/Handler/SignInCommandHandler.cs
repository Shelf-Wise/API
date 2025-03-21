using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Authentication.Command;
using LibraryManagement.Application.Features.Authentication.Dto;
using LibraryManagement.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryManagement.Application.Features.Authentication.Handler
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediatR;

        public SignInCommandHandler(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IMediator mediatR
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mediatR = mediatR;
        }

        public async Task<Result> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);

            if (user == null)
                return Result.Failure(new Error("404", "This user is not SignedUp"));

            SignInResult result = await _signInManager.PasswordSignInAsync(
                user.UserName!,
                command.Password,
                true,
                false
            );

            if (!result.Succeeded)
            {
                string errorMessage = "Authentication failed: ";

                if (result.IsLockedOut)
                    errorMessage += "Account is locked out.";
                else if (result.IsNotAllowed)
                    errorMessage += "Login not allowed.";
                else if (result.RequiresTwoFactor)
                    errorMessage += "Two-factor authentication required.";
                else
                    errorMessage += "Invalid credentials.";

                return Result.Failure(new Error("403", errorMessage));
            }
            List<Claim> authClaims = new List<Claim>
            {
                new(ClaimTypes.Email, command.Email),
                new(ClaimTypes.UserData, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var Token = await _mediatR.Send(
                new GenerateTokenCommand(authClaims),
                cancellationToken
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(Token);

            var response = new SignInResponse();

            response.Email = user.Email;
            response.Username = user.UserName;
            response.token = tokenString;

            return Result.Success(response);
        }
    }
}
