using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Data.Models;

public class Question : BaseEntity
{
    public Question()
    {
    }

    public Question(int createdBy, string text, int quizId)
    {
        CreatedBy = createdBy;
        Text = text;
        QuizId = quizId;
    }

    [Required] public string Text { get; set; }

    [ForeignKey("Quiz")] public int QuizId { get; set; }

    public virtual Quiz Quiz { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }

    public void Update(int updatedBy, string text, int quizId, Quiz quiz)
    {
        UpdatedBy = updatedBy;
        Text = text;
        QuizId = quizId;
        Quiz = quiz;
        UpdatedTime = DateTime.UtcNow;
    }
}