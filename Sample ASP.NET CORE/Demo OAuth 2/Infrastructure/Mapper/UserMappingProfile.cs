using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, src => src.MapFrom(src => src.Password));

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Gender, src => src.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Role, src => src.MapFrom(src => src.Role.ToString()));

            CreateMap<User, ReturnLoginUserDTO>();
        }
    }
}
