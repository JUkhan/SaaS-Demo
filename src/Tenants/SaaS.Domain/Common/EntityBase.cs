using System;
namespace SaaS.Domain.Common
{
    public class EntityBase
    {
        public int Id { get; protected set; }
        
        public string TenantId { get; set; }
    }
}
