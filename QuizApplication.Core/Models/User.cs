using System.ComponentModel.DataAnnotations;

namespace QuizApplication.Core.Models;

public class User : BaseEntity
{
    [Required]
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    public void update(string fullName, string email, string password)
    {
        UserName = fullName;
        Email = email;
        Password = password;
        UpdatedTime = DateTime.UtcNow;
    }
}