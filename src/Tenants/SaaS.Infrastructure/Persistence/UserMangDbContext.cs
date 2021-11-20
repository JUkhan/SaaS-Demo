using System;
using Microsoft.EntityFrameworkCore;
using SaaS.Domain.Entities.UserManagement;

namespace SaaS.Infrastructure.Persistence
{
    public class UserMangDbContext: DbContext
    {
       
        public UserMangDbContext(DbContextOptions<UserMangDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
