using Microsoft.EntityFrameworkCore;
using QuizApplication.Application.Dtos;
using QuizApplication.Core.Models;
using QuizApplication.Data;
using QuizApplication.Data.Repositories;

namespace QuizApplication.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> CreateAsync(string fullName, string email, string password)
    {
        var user = new User(fullName, email, password);
        await _userRepository.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync();
        var result = await _userRepository.GetAsync(x => x.Id == user.Id).FirstOrDefaultAsync();
        return UserDto.Map(result);
    }

    public async Task<List<UserDto>> GetAsync(string fullname, string email, string password)
    {
        var user = _userRepository.GetAsync(x => true);
        if (!string.IsNullOrWhiteSpace(fullname))
            user = user.Where(x => x.FullName == fullname);
        if (!string.IsNullOrWhiteSpace(email))
            user = user.Where(x => x.FullName == email);
        if (!string.IsNullOrWhiteSpace(password))
            user = user.Where(x => x.FullName == password);
        return user.Select(x => UserDto.Map(x)).ToList();
    }

    public async Task<UserDto> UpdateAsync(int id, string fullName, string email, string password)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id).FirstOrDefaultAsync();
        user.Update(fullName, email, password);
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
}