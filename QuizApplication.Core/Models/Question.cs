using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Core.Models;

public class Question : BaseEntity
{
    public Question()
    {
    }

    public Question(string text, int quizId)
    {
        Text = text;
        QuizId = quizId;
    }

    [Required] public string Text { get; set; }

    [ForeignKey("Quiz")] public int QuizId { get; set; }

    public virtual Quiz Quiz { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }

    public void Update(string text, int quizId, Quiz quiz)
    {
        Text = text;
        QuizId = quizId;
        Quiz = quiz;
        UpdatedTime = DateTime.UtcNow;
    }
}