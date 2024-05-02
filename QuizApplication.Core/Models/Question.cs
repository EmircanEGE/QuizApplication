namespace QuizApplication.Core.Models
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public void Update(string content)
        {
            Content = content;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
