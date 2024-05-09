using Microsoft.EntityFrameworkCore;
using QuizApplication.Api;
using QuizApplication.Application.Dtos;
using QuizApplication.Application.Extensions;
using QuizApplication.Application.Models;
using QuizApplication.Data;
using QuizApplication.Data.Models;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class UserService : IUserService
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

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
        return new ApiResponse<UserDto>(201);
    }

    public async Task<ApiResponse<UserDto>> UpdateAsync(int id, string fullName, string email, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (user == null) return new ApiResponse<UserDto>(404, "User not found!");
        var passwordHash = PasswordHasher.Hash(password);
        user.Update(fullName, email, passwordHash);
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<UserDto>(200, UserDto.Map(user));
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (user == null) return new ApiResponse<bool>(404, "User not found!");
        _userRepository.Delete(user);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<bool>(204);
    }

    public async Task<ApiResponse<UserDto>> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (user == null) return new ApiResponse<UserDto>(404, "User not found!");
        return new ApiResponse<UserDto>(200, UserDto.Map(user));
    }

    public async Task<ApiResponse<List<UserDto>>> GetAsync(string fullname, string email)
    {
        var user = _userRepository.GetAsync(x => true);
        if (!string.IsNullOrWhiteSpace(fullname))
            user = user.Where(x => x.FullName == fullname);
        if (!string.IsNullOrWhiteSpace(email))
            user = user.Where(x => x.Email == email);
        return new ApiResponse<List<UserDto>>(200, user.Select(x => UserDto.Map(x)).ToList());
    }
}