using QuizApplication.Core.Models;

namespace QuizApplication.Data.Repositories;

public class AnswerRepository : Repository<Answer>, IAnswerRepository
{
    public AnswerRepository(Context context) : base(context)
    {
    }
}