using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Core.Models;

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

    [Required] public string FullName { get; set; }

    [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }

    public void Update(string fullName, string email, string password)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        UpdatedTime = DateTime.UtcNow;
    }
}