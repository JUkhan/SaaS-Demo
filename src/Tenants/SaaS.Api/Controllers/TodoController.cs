using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaaS.Application.Extensions;
using SaaS.Application.Models;
using static SaaS.Application.Features.Todos.AddTodo;
using static SaaS.Application.Features.Todos.AllTodos;
using static SaaS.Application.Features.Todos.UpdateTodo;

namespace SaaS.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var tenantinfo = GetTenantInfo(); //HttpContext.Session.GetObjectFromJson<TenantInfo>("tenantInfo");
            
            return Ok(await _mediator.Send(new QueryAllTodos(tenantinfo.TenantId)));
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] CommandAddTodo command)
        {
            var tenantinfo = GetTenantInfo(); //HttpContext.Session.GetObjectFromJson<TenantInfo>("tenantInfo");
            return Ok(await _mediator.Send(command with {TenantId= tenantinfo.TenantId }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo([FromBody] CommandUpdateTodo command)
        {
            return Ok(await _mediator.Send(command));
        }

        private TenantInfo GetTenantInfo() => (TenantInfo)HttpContext.Items["User"];

    }
}
