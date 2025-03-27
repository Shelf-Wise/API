using LibraryManagement.Application.Abstractions.Messaging;
using LibraryManagement.Application.Features.Authentication.Command;
using LibraryManagement.Application.Shared;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Application.Features.Authentication.Handler
{
    public class SignUpCommandHandler : ICommandHandler<SignUpCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SignUpCommandHandler(
            UserManager<IdentityUser> userManager
        )
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByEmailAsync(command.Email);
            if (userExists != null)
                return Result.Failure(new Error("400", "Email already exists"));

            var userExists2 = await _userManager.FindByNameAsync(command.UserName);
            if (userExists2 != null)
                return Result.Failure(new Error("400", "Username already exists"));

            var user = new IdentityUser
            {
                UserName = command.UserName,
                Email = command.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            // if (!await _roleManager.RoleExistsAsync(command.Role))
            //   return Result.Failure(new Error("400", "Invalid role"));

            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
                return Result.Failure(new Error("400", "User creation failed"));

            //  await _userManager.AddToRoleAsync(user, command.Role);

            return Result.Success("Signed Up Successfully");
        }
    }
}
