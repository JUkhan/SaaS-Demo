using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SaaS.Application.Contracts.Persistence;
using SaaS.Domain.Entities.UserManagement;

namespace SaaS.Application.Features.Users
{
    public static class FindUser
    {
        public record QueryFindUser(string UserName, string Passworrd): IRequest<User> { }

        public class FUHandler : IRequestHandler<QueryFindUser, User>
        {
            private IUserRepository _userRepository;

            public FUHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            }

            public async Task<User> Handle(QueryFindUser request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAsync(user => user.UserName == request.UserName && user.Password == request.Passworrd);
                return users.Count > 0 ? users[0] : null;
            }
        }
    }
}
