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
        public UserMappingProfile()
        {
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.Gender, src => src.MapFrom(src => Enum.Parse<Gender>(src.Gender)));

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role, src => src.MapFrom(src => src.Role.ToString()))
                .ForMember(dest => dest.Gender, src => src.MapFrom(src => src.Gender.ToString()))
                .ReverseMap();
        }
    }
}
