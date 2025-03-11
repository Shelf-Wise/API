using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

namespace LibraryMngementC.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection ConfigureIdentityService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<IdentityDatabaseContext>(options =>
            {
                options
                    .UseNpgsql(configuration.GetConnectionString("PostgresConnection"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

            services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<IdentityDatabaseContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)
                        ),
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPriviledges", p => p.RequireRole("LibraryStaffManagement"));
                options.AddPolicy(
                    "LowLevelPriviledges",
                    p => p.RequireRole("LibraryStaffMinor", "LibraryStaffManagement")
                );
            });

            return services;
        }
    }
}
