using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Data.Models;

public class Answer : BaseEntity
{
    public Answer()
    {
    }

    public Answer(int createdBy, string text, bool isCorrect, int questionId)
    {
        CreatedBy = createdBy;
        Text = text;
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }

    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    [ForeignKey("Question")] public int QuestionId { get; set; }
    public virtual Question Question { get; set; }

    public void Update(int updatedBy, string text, bool isCorrect, int questionId, Question question)
    {
        UpdatedBy = updatedBy;
        Text = text;
        IsCorrect = isCorrect;
        QuestionId = questionId;
        Question = question;
        UpdatedTime = DateTime.UtcNow;
    }
}