using System;
using SaaS.Domain.Entities.MultiTenants;

namespace SaaS.Application.Contracts.Persistence
{
    public interface ITodoRepository : IAsyncRepository<Todo>
    {
       
    }
}
