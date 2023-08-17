using Application.DTOs.User;
using AutoMapper;
using Domain.Enum;
using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        protected UserMappingProfile()
        {
            CreateMap<CreateUserDTO, User>()
                .ReverseMap();
            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}
