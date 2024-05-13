using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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
    public int LoggedInUserId;
    public readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        LoggedInUserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("userId"));
    }

    public async Task<ApiResponse<UserDto>> CreateAsync(string fullName, string email, string password)
    {
        var passwordHash = PasswordHasher.Hash(password);
        var user = new User(LoggedInUserId, fullName, email, passwordHash);
        await _userRepository.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<UserDto>(201);
    }

    public async Task<ApiResponse<UserDto>> UpdateAsync(int id, string fullName, string email, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (user == null) return new ApiResponse<UserDto>(404, "User not found!");
        var passwordHash = PasswordHasher.Hash(password);
        user.Update(LoggedInUserId, fullName, email, passwordHash);
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

    public async Task<ApiResponse<List<UserDto>>> GetAsync(string fullname, string email, int page, int pageSize)
    {
        if (page == 0 || pageSize == 0)
            return new ApiResponse<List<UserDto>>(400, "Page or page size must be greater than 0");
        var user = _userRepository.GetAsync(x => true);
        if (!string.IsNullOrWhiteSpace(fullname))
            user = user.Where(x => x.FullName == fullname);
        if (!string.IsNullOrWhiteSpace(email))
            user = user.Where(x => x.Email == email);

        var userDto = user.Select(x => UserDto.Map(x)).ToList();
        var totalCount = userDto.Count();
        var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
        if (page > totalPages) return new ApiResponse<List<UserDto>>(404, "Page not found");
        var userPerPage = userDto.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new ApiResponse<List<UserDto>>(200, userPerPage);
    }
}