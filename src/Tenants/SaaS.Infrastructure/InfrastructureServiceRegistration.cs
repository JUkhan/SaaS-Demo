using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaaS.Application.Extensions;
using SaaS.Application.Contracts.Persistence;
using SaaS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using SaaS.Application.Models;
using SaaS.Infrastructure.Repositories;
using SaaS.Domain.Entities.UserManagement;
using SaaS.Domain.Entities.MultiTenants;

namespace SaaS.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserMangDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("UserConnectionString")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<TenantDbContext>((serviceProvider, dbContextBuilder) =>
            {

                var connectionStringPlaceHolder = configuration.GetConnectionString("TenantConnectionString");
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var obj = httpContextAccessor.HttpContext.Session.GetObjectFromJson<TenantInfo>("tenantInfo");

                var connectionString = connectionStringPlaceHolder.Replace("{dbName}", obj.DatabaseName);
                dbContextBuilder.UseSqlServer(connectionString);
            });
            services.AddScoped(typeof(IAsyncRepository<>), typeof(TenantsRepositoryBase<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(UMRepositoryBase<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            

            return services;
        }
    }
}
