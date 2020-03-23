using Microsoft.Extensions.DependencyInjection;
using Tutor101.Service.Services.Security;

namespace Tutor101.Initializers
{
    public static class ServiceInjector
    {
        public static void InjectServices(IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();
        }
    }
}
