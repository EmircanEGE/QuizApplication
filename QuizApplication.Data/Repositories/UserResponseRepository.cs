using QuizApplication.Core.Models;

namespace QuizApplication.Data.Repositories;

public class UserResponseRepository : Repository<UserResponse>, IUserResponseRepository
{
    public UserResponseRepository(Context context) : base(context)
    {
    }
}