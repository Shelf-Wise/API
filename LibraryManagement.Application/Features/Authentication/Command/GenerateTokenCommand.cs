using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;

namespace LibraryManagement.Application.Features.Authentication.Command
{
    public class GenerateTokenCommand(List<Claim> authClaims) : IRequest<JwtSecurityToken>
    {
        public List<Claim> AuthClaims { get; } = authClaims;
    }
}
