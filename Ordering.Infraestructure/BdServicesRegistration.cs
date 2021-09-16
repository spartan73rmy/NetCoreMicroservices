using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Infraestructure.Persistence;

namespace Ordering.Infraestructure
{
    public static class BdServicesRegistration
    {
        public static IServiceCollection AddADbServices(this IServiceCollection services)
        {

            return services;
        }
    }
}
