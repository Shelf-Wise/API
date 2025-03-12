using LibraryManagement.Application.Abstractions.Persistence;
using LibraryManagementC.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace LibraryManagementC.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<ApplicationWriteDbContext>(options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("SupabaseConnection"),
                        b =>
                        {
                            b.MigrationsAssembly(
                                typeof(ApplicationWriteDbContext).Assembly.FullName
                            );
                            b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        }
                    )
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

            services.AddScoped<IDbConnection>(
                (sp) =>
                {
                    return new NpgsqlConnection(
                        configuration.GetConnectionString("PostgresConnection")
                    );
                }
            );

            services.AddScoped(typeof(IGenericWriteRepository<>), typeof(GenericWriteRepository<>));
            services.AddScoped(typeof(IGenericReadRepository<>), typeof(GenericReadRepository<>));
            services.AddScoped(
                typeof(ILibraryMemberReadRepository),
                typeof(LibraryMemberReadRepositiory)
            );
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            return services;
        }
    }
}
