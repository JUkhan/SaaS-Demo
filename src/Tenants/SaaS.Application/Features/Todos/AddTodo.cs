using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SaaS.Application.Contracts.Persistence;
using SaaS.Domain.Entities.MultiTenants;

namespace SaaS.Application.Features.Todos
{
    public static class AddTodo
    {
        public record CommandAddTodo(string Title, string TenantId) : IRequest<Todo> { }

        public class AddTodoHandler : IRequestHandler<CommandAddTodo, Todo>
        {
            private ITodoRepository _todoRepository;
            private readonly IMapper _mapper;

            public AddTodoHandler(ITodoRepository todoRepository, IMapper mapper)
            {
                _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public Task<Todo> Handle(CommandAddTodo request, CancellationToken cancellationToken)
            {
                var todo = _mapper.Map<Todo>(request);
               return _todoRepository.AddAsync(todo);
            }
        }
    }
}
