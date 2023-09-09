using Application;
using Application.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
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

        public async Task<ReturnLoginUserDTO?> CreateNewAsync(CreateUserDTO createUserDto)
        {
            var userObj = _mapper.Map<User>(createUserDto);

            await _unitOfWork.UserRepository.AddAsync(userObj);
            var success = await _unitOfWork.SaveChangeAsync();

            if (success <= 0) return null;

            var createdUserObj = await _unitOfWork.UserRepository.GetByIdAsync(userObj.Id);
            // var returnUser = _mapper.Map<UserDTO>(createdUserObj);
            // return returnUser;
            var mappingTask = Task.Run(() => _mapper.Map<ReturnLoginUserDTO>(createdUserObj));
            var tokenTask = _jwtService.GenerateAccessTokenAsync(createdUserObj);

            await Task.WhenAll(mappingTask, tokenTask);

            var result = mappingTask.Result;
            var token = tokenTask.Result;

            result.Token = token;
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDTO>>(users);
            return userDtos;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<ReturnLoginUserDTO?> LoginAsync(UserLogin userLogin)
        {
            var user = await _unitOfWork.UserRepository.LoginAsync(userLogin);

            if (user == null) return null;

            var mappingTask = Task.Run(() => _mapper.Map<ReturnLoginUserDTO>(user));
            var tokenTask = _jwtService.GenerateAccessTokenAsync(user);

            await Task.WhenAll(mappingTask, tokenTask);

            var result = mappingTask.Result;
            var token = tokenTask.Result;

            result.Token = token;
            return result;
        }
    }
}