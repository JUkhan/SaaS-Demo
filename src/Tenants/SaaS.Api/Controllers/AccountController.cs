using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SaaS.Application.Models;
using static SaaS.Application.Features.Users.AddUser;
using SaaS.Application.Extensions;
using SaaS.Infrastructure.Persistence;
using SaaS.Domain.Entities.UserManagement;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using SaaS.Api.Helpers;

namespace SaaS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public AccountController(IMediator mediator, IServiceProvider serviceProvider, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] CommandAU command)
        {
            var user = await _mediator.Send(command);

            // set session for tenant info
            var tenantInfo = new TenantInfo { DatabaseName = user.DatabaseName, TenantId = user.TenantId };
            HttpContext.Session.SetObjectAsJson("tenantInfo", tenantInfo);

            //create tenant database and push dummy data
            var tenantDbContext = _serviceProvider.GetRequiredService <TenantDbContext>();
            await tenantDbContext.CrreateDatabaseAndPushDummyData(user.TenantId);

            return Ok(new { dbName=user.DatabaseName, userName=user.UserName, token= generateJwtToken(user) });
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("database", user.DatabaseName) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
