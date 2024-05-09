using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Data.Models;

public class Answer : BaseEntity
{
    public Answer()
    {
    }

    public Answer(string text, bool isCorrect, int questionId)
    {
        Text = text;
        IsCorrect = isCorrect;
        QuestionId = questionId;
    }

    [Required] public string Text { get; set; }

    public bool IsCorrect { get; set; }
    [ForeignKey("Question")] public int QuestionId { get; set; }
    public virtual Question Question { get; set; }

    public void Update(string text, bool isCorrect, int questionId, Question question)
    {
        Text = text;
        IsCorrect = isCorrect;
        QuestionId = questionId;
        Question = question;
        UpdatedTime = DateTime.UtcNow;
    }
}