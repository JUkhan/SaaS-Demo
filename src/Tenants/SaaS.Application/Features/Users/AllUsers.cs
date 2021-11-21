using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SaaS.Application.Contracts.Persistence;
using SaaS.Domain.Entities.UserManagement;

namespace SaaS.Application.Features.Users
{
    public class AllUsers
    {
       public class QueryAllUser : IRequest<IEnumerable<User>> { }

        public class AllUserHandler : IRequestHandler<QueryAllUser, IEnumerable<User>>
        {
            private IUserRepository _userRepository;

            public AllUserHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            }

            public async Task<IEnumerable<User>> Handle(QueryAllUser request, CancellationToken cancellationToken)
            {
                var res=await _userRepository.GetAllAsync();

                return res;
            }
        }
    }
}
