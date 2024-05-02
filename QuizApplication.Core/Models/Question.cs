using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Core.Models;

public class Question : BaseEntity
{
    [Required]
    public string Text { get; set; }

    [ForeignKey("Quiz")] public int QuizId { get; set; }

    public virtual Quiz Quiz { get; set; }

    [ForeignKey("Answer")] public int CorrectAnswer { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }

    public void Update(string text)
    {
        Text = text;
        UpdatedTime = DateTime.UtcNow;
    }
}