using QuizApplication.Data.Models;

namespace QuizApplication.Data.Repositories;

public class QuestionRepository : Repository<Question>, IQuestionRepository
{
    public QuestionRepository(Context context) : base(context)
    {
    }
}