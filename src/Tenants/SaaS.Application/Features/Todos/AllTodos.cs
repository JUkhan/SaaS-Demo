using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SaaS.Application.Contracts.Persistence;
using SaaS.Domain.Entities.MultiTenants;

namespace SaaS.Application.Features.Todos
{
    public static class AllTodos
    {
        public record QueryAllTodos(string TenantId) : IRequest<IEnumerable<Todo>> { }

        public class HandlerAllTodos : IRequestHandler<QueryAllTodos, IEnumerable<Todo>>
        {
            private ITodoRepository _todoRepository;

            public HandlerAllTodos(ITodoRepository todoRepository)
            {
                _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
            }

            public async Task<IEnumerable<Todo>> Handle(QueryAllTodos request, CancellationToken cancellationToken)
            {
                var todos = await _todoRepository.GetAsync(todo => todo.TenantId == request.TenantId);
                return todos;
            }
        }
    }
}
