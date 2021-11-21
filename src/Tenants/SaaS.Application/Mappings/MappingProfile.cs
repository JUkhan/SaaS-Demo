using System;
using AutoMapper;
using SaaS.Domain.Entities.MultiTenants;
using SaaS.Domain.Entities.UserManagement;
using static SaaS.Application.Features.Todos.AddTodo;
using static SaaS.Application.Features.Users.AddUser;

namespace SaaS.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CommandAU>().ReverseMap();
            CreateMap<Todo, CommandAddTodo>().ReverseMap();
        }
    }
}
