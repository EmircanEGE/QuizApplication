namespace QuizApplication.Data.Models;

public class User : BaseEntity
{
    public User()
    {
    }

    public User(int createdBy, string fullName, string email, string password)
    {
        CreatedBy = createdBy;
        FullName = fullName;
        Email = email;
        Password = password;
    }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public void Update(int updatedBy, string fullName, string email, string password)
    {
        UpdatedBy = updatedBy;
        FullName = fullName;
        Email = email;
        Password = password;
        UpdatedTime = DateTime.UtcNow;
    }
}