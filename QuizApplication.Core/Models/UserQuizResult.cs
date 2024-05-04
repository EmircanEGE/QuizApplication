using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Core.Models;

public class UserQuizResult : BaseEntity
{
    [ForeignKey("User")] public int UserId { get; set; }

    public virtual User User { get; set; }

    [ForeignKey("Quiz")] public int QuizId { get; set; }
    public int Score { get; set; }
    public virtual Quiz Quiz { get; set; }
}