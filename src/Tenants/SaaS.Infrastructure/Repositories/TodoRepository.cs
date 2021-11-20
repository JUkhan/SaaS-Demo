using System;
using SaaS.Application.Contracts.Persistence;
using SaaS.Domain.Entities.MultiTenants;
using SaaS.Infrastructure.Persistence;

namespace SaaS.Infrastructure.Repositories
{
    public class TodoRepository : TenantsRepositoryBase<Todo>, ITodoRepository
    {
        public TodoRepository(TenantDbContext dbContet) : base(dbContet)
        {

        }
    }
   
}
