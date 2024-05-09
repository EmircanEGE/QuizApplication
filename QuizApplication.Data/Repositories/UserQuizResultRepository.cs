using QuizApplication.Data.Models;

namespace QuizApplication.Data.Repositories;

public class UserQuizResultRepository : Repository<UserQuizResult>, IUserQuizResultRepository
{
    public UserQuizResultRepository(Context context) : base(context)
    {
    }
}