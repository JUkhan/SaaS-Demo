using System;
using AutoMapper;
using SaaS.Domain.Entities.UserManagement;
using static SaaS.Application.Features.Users.AddUser;

namespace SaaS.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CommandAU>().ReverseMap();
        }
    }
}
