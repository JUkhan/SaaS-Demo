using System;
namespace SaaS.Application.Models
{
    public class TenantInfo
    {
        public TenantInfo()
        {
        }

        public string DatabaseName { get; set; }
        public string TenantId { get; set; }
    }
}
