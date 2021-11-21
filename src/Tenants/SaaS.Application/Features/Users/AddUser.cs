using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SaaS.Application.Contracts.Persistence;
using SaaS.Domain.Entities.UserManagement;
using SaaS.Application.Extensions;
using SaaS.Application.Models;


namespace SaaS.Application.Features.Users
{
    public static class AddUser
    {
        public record CommandAU(string FirstName,string LastName,string UserName,string Email, string Password, bool IsPayingUser) : IRequest<User>{ }

        public class AUHandler : IRequestHandler<CommandAU, User>
        {
            private IUserRepository _userRepository;
            private readonly IMapper _mapper;
           

            public AUHandler(IUserRepository userRepository, IMapper mapper, IServiceProvider serviceProvider)
            {
                _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                
            }

            public async Task<User> Handle(CommandAU request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request);

                // initiallize tenant id and database name
                if (user.IsPayingUser)
                {
                    user.TenantId = string.Empty;
                    user.DatabaseName = Guid.NewGuid().ToString();
                }
                else
                {
                    user.TenantId = Guid.NewGuid().ToString();
                    user.DatabaseName = "SharedDB";
                }

                //Save user record
                user=await _userRepository.AddAsync(user);

                return user;
            }
        }
    }
}
