using System;
using SaaS.Domain.Entities.UserManagement;

namespace SaaS.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}
