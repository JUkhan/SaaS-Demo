using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SaaS.Application.Contracts.Persistence;
using SaaS.Domain.Entities.MultiTenants;

namespace SaaS.Application.Features.Todos
{
    public static class UpdateTodo
    {
        public record CommandUpdateTodo(int Id, bool Completed) : IRequest<bool> { }

        public class UpdateTodoHandler : IRequestHandler<CommandUpdateTodo, bool>
        {
            private ITodoRepository _todoRepository;
            private readonly IMapper _mapper;

            public UpdateTodoHandler(ITodoRepository todoRepository, IMapper mapper)
            {
                _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<bool> Handle(CommandUpdateTodo request, CancellationToken cancellationToken)
            {
                
                var todo= await _todoRepository.GetByIdAsync(request.Id);
                if (todo != null)
                {
                    todo.Completed = request.Completed;
                    await _todoRepository.UpdateAsync(todo);
                    return true;
                }
                return false;
            }

           
        }
    }
}
