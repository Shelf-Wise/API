using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Authentication.Command;
using LibraryManagement.Application.Shared;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Application.Features.Authentication.Handler
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SignUpCommandHandler(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByNameAsync(command.Username);
            if (userExists != null)
                return Result.Failure(new Error("400", "Username already exists"));

            var user = new IdentityUser
            {
                UserName = command.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            if (!await _roleManager.RoleExistsAsync(command.Role))
                return Result.Failure(new Error("400", "Invalid role"));

            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
                return Result.Failure(new Error("400", "User creation failed"));

            await _userManager.AddToRoleAsync(user, command.Role);

            return Result.Success("Signed Up Successfully");
        }
    }
}
