using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Core.Models;

public class Quiz : BaseEntity
{
    public Quiz()
    {
    }

    public Quiz(string title, string description, int userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
    }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [ForeignKey("User")] public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Question> Questions { get; set; }

    public void Update(string title, string description, int userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
        UpdatedTime = DateTime.UtcNow;
    }
}