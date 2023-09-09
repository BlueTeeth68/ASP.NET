using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, src => src.MapFrom(src => BCrypt.Net.BCrypt.EnhancedHashPassword(src.Password)));

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Gender, src => src.MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Role, src => src.MapFrom(src => src.Role.ToString()));

            CreateMap<User, ReturnLoginUserDTO>();
        }
    }
}
