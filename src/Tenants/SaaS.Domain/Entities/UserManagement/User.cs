using System;
using SaaS.Domain.Common;

namespace SaaS.Domain.Entities.UserManagement
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string DatabaseName { get; set; }
       
        public bool IsPayingUser { get; set; }

        
    }
}
