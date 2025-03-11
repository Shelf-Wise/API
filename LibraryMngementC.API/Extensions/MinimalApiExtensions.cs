using LibraryMngementC.API.Interfaces;

namespace LibraryMngementC.API.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void MapEndPoint(this WebApplication app)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var classes = assemblies
                .Distinct()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IApiModule).IsAssignableFrom(x) && !x.IsAbstract);

            foreach (var assembly in classes)
            {
                {
                    var instance = Activator.CreateInstance(assembly) as IApiModule;
                    instance?.MapEndEndpoint(app);
                }
            }
        }
    }
}
