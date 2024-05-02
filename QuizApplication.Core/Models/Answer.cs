using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Core.Models
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public void Update(string content, int questionId)
        {
            Content = content;
            QuestionId = questionId;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
