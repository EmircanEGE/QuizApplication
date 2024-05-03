using QuizApplication.Core.Models;

namespace QuizApplication.Data.Repositories;

public class QuizRepository : Repository<Quiz>, IQuizRepository
{
    public QuizRepository(Context context) : base(context)
    {
    }
}