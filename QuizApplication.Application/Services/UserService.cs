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

    public async Task<UserDto> CreateAsync(string fullName, string email, string password)
    {
        var passwordHash = PasswordHasher.Hash(password);
        var user = new User(fullName, email, passwordHash);
        await _userRepository.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync();
        var result = await _userRepository.GetAsync(x => x.Id == user.Id).FirstOrDefaultAsync();
        return UserDto.Map(result);
    }
    
    public async Task<UserDto> UpdateAsync(int id, string fullName, string email, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if (user == null) return new UserDto();
        var passwordHash = PasswordHasher.Hash(password);
        user.Update(fullName, email, passwordHash);
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return UserDto.Map(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        _userRepository.Delete(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        if(user == null) return new UserDto();
        return UserDto.Map(user);
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

    public async Task<string> AuthenticateAsync(string email, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        if (user == null) return null;
        var userDto = UserDto.Map(user);
        return _tokenService.GenerateToken(userDto);
    }
}