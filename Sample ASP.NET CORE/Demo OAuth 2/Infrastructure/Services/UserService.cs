using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<UserDTO> CreateNewAsync(CreateUserDTO createUserDTO)
        {
            var userObj = _mapper.Map<User>(createUserDTO);
            await _unitOfWork.UserRepository.AddAsync(userObj);
            var result = await _unitOfWork.SaveChangeAsync();
            if(result > 0)
            {
                var createdUserObj = await _unitOfWork.UserRepository.GetByIdAsync(userObj.Id);
                var returnUser = _mapper.Map<UserDTO>(createdUserObj);
                return returnUser;
            }
            return null;
        }

        public async Task<int> DeleteAsync(int id)
        {
            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var userDTOs = _mapper.Map<List<UserDTO>>(users);
            return userDTOs;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<ReturnLoginUserDTO> LoginAsync(UserLogin userLogin)
        {
            var user = await _unitOfWork.UserRepository.LoginAsync(userLogin);
            if(user != null)
            {
                var mappingTask = Task.Run(() => _mapper.Map<ReturnLoginUserDTO>(user));
                var tokenTask = _jwtService.GenerateAccessTokenAsync(user);

                await Task.WhenAll(mappingTask, tokenTask);

                var result = mappingTask.Result;
                var token = tokenTask.Result;

                result.Token = token;
                return result;
            }
            return null;
        }
    }
}
