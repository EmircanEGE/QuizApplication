using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Core.Models
{
    public class UserResponse : BaseEntity
    {
        
        [ForeignKey("UserQuizResult")] public int QuizResultId { get; set; }
        public virtual UserQuizResult UserQuizResult { get; set; }
        [ForeignKey("Question")] public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        [ForeignKey("Answer")] public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
