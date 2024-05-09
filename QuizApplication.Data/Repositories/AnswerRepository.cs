using QuizApplication.Data.Models;

namespace QuizApplication.Data.Repositories;

public class AnswerRepository : Repository<Answer>, IAnswerRepository
{
    public AnswerRepository(Context context) : base(context)
    {
    }
}