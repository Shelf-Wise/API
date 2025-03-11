using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Authentication.Command;
using LibraryManagement.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Application.Features.Authentication.Handler
{
    public class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediatR;

        public SignInCommandHandler(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMediator mediatR
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mediatR = mediatR;
        }

        public async Task<Result> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(command.Username);

            if (user == null)
                return Result.Failure(new Error("404", "This user is not SignedUp"));

            SignInResult result = await _signInManager.PasswordSignInAsync(
                command.Username,
                command.Password,
                true,
                false
            );

            if (!result.Succeeded)
                return Result.Failure(new Error("403", result.ToString()));

            var userRole = await _userManager.GetRolesAsync(user);

            if (userRole[0] != command.Role)
                return Result.Failure(new Error("403", "User does not have the required role"));

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, command.Username),
                new(ClaimTypes.Role, command.Role),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var Token = await _mediatR.Send(
                new GenerateTokenCommand(authClaims),
                cancellationToken
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(Token);

            return Result.Success(tokenString);
        }
    }
}
