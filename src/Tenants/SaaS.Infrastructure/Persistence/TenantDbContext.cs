using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaaS.Domain.Entities.MultiTenants;

namespace SaaS.Infrastructure.Persistence
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }

        public async Task CrreateDatabaseAndPushDummyData(string tenantId)
        {
            await Database.EnsureCreatedAsync();

            await Todos.AddRangeAsync(
                new Todo { Title = "task1" , TenantId=tenantId  },
                new Todo { Title = "task2", TenantId = tenantId },
                new Todo { Title = "task3", TenantId = tenantId }
                );

            await SaveChangesAsync();
        }
    }
}
