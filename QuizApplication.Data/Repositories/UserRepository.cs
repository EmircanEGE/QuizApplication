using QuizApplication.Data.Models;

namespace QuizApplication.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(Context context) : base(context)
    {
    }
}