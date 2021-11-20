using System;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities.MultiTenants
{
    public class Todo : EntityBase
    {
        public string Title { get; set; }

        public bool Completed { get; set; }
    }
}
