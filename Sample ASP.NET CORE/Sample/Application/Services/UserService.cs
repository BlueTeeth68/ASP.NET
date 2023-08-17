using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Enum;
using Sample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUser(CreateUserDTO createUserDTO)
        {
            var userObj = _mapper.Map<User>(createUserDTO);
            await userRepository.AddAsync(userObj);
            var result = await userRepository.SaveChangesAsync();
            if (result > 0)
            {
                var createdUser = await GetByIdAsync(userObj.Id);
                var returnUserDTO = _mapper.Map<UserDTO>(createdUser);
                return returnUserDTO;
            }
            return null;
        }

        public async Task DeleteUser(int id)
        {
            await userRepository.DeleteById(id);
            await userRepository.SaveChangesAsync();
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();
            var userDTOs = _mapper.Map<List<UserDTO>>(users);
            return userDTOs;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> UpdateUser(int id, UpdateUserDTO updateUserDTO)
        {
            if(id != updateUserDTO.Id)
            {
                //throw exceptions
                return null;
            }
            var userObj = await userRepository.GetByIdAsync(id);
            if (updateUserDTO.FullName != null) { 
                userObj.FullName = updateUserDTO.FullName;
            } 

            if(updateUserDTO.Gender != null) {
                userObj.Gender = Enum.Parse<Gender>(updateUserDTO.Gender);
            }

            if (updateUserDTO.Role != null)
            {
                userObj.Role = Enum.Parse<Role>(updateUserDTO.Role);
            }

            userRepository.Update(userObj);
            var result = await userRepository.SaveChangesAsync();
            if(result > 0 )
            {
                var updateUser = await userRepository.GetByIdAsync(id);
                var returnUserDTO = _mapper.Map<UserDTO>(updateUser);
                return returnUserDTO;
            }
            
            return null;
        }
    }
}
