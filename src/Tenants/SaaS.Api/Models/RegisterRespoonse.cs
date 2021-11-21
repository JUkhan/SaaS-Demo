using System;
namespace SaaS.Api.Models
{
    public record RegisterResponse(string UserName, string DatabaseName, string Token);
   
}
