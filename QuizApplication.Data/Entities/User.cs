namespace QuizApplication.Data.Models;

public class User : BaseEntity
{
    public User()
    {
    }

    public User(string fullName, string email, string password)
    {
        FullName = fullName;
        Email = email;
        Password = password;
    }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public void Update(string fullName, string email, string password)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        UpdatedTime = DateTime.UtcNow;
    }
}