using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

using MediatR;
namespace SaaS.Application
{
    public static class ApplicatioServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(ApplicatioServiceRegistration).Assembly);
            return services;
        }
    }
}
