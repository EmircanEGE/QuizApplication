using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Data.Models;

public class Quiz : BaseEntity
{
    public Quiz()
    {
    }

    public Quiz(int createdBy, string title, string description, int userId)
    {
        CreatedBy = createdBy;
        Title = title;
        Description = description;
        UserId = userId;
    }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Question> Questions { get; set; }

    public void Update(int updatedBy, string title, string description, int userId, User user)
    {
        UpdatedBy = updatedBy;
        Title = title;
        Description = description;
        UserId = userId;
        User = user;
        UpdatedTime = DateTime.UtcNow;
    }
}