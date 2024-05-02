namespace QuizApplication.Core.Models
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

        public void update(string fullName, string email, string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
