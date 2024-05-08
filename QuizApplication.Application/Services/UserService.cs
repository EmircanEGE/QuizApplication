using Microsoft.EntityFrameworkCore;
using QuizApplication.Api;
using QuizApplication.Application.Dtos;
using QuizApplication.Application.Extensions;
using QuizApplication.Core.Models;
using QuizApplication.Data;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<ApiResponse<UserDto>> CreateAsync(string fullName, string email, string password)
    {
        var passwordHash = PasswordHasher.Hash(password);
        var user = new User(fullName, email, passwordHash);
        await _userRepository.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<UserDto>(201, "User created successfully.", UserDto.Map(user));
    }
    
    public async Task<ApiResponse<UserDto>> UpdateAsync(int id, string fullName, string email, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (user == null) return new ApiResponse<UserDto>(404, $"User id = {id} not found!", new UserDto());
        var passwordHash = PasswordHasher.Hash(password);
        user.Update(fullName, email, passwordHash);
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<UserDto>(204, "User updated successfully.", UserDto.Map(user));
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        _userRepository.Delete(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ApiResponse<UserDto>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if(user == null) return new ApiResponse<UserDto>(404, $"User id = {id} not found!", new UserDto());
        return new ApiResponse<UserDto>(200, "", UserDto.Map(user));
    }

    public async Task<List<UserDto>> GetAsync(string fullname, string email)
    {
        var user = _userRepository.GetAsync(x => true);
        if (!string.IsNullOrWhiteSpace(fullname))
            user = user.Where(x => x.FullName == fullname);
        if (!string.IsNullOrWhiteSpace(email))
            user = user.Where(x => x.Email == email);
        return user.Select(x => UserDto.Map(x)).ToList();
    }

    public async Task<LoginResponse> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        if (user == null) return null;
        var userDto = UserDto.Map(user);
        var response = new LoginResponse();
        response.Token = _tokenService.GenerateToken(userDto);
        return response;
    }
}