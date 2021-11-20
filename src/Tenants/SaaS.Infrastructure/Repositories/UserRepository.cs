using System;
using SaaS.Application.Contracts.Persistence;
using SaaS.Domain.Entities.UserManagement;
using SaaS.Infrastructure.Persistence;

namespace SaaS.Infrastructure.Repositories
{
    public class UserRepository : UMRepositoryBase<User>, IUserRepository
    {
        public UserRepository(UserMangDbContext dbContet) : base(dbContet)
        {

        }

    }
}
