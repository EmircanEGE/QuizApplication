using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Core.Models;

public class Quiz : BaseEntity
{
    public Quiz()
    {
    }

    public Quiz(string title, string description, int createdBy)
    {
        Title = title;
        Description = description;
        CreatedBy = createdBy;
    }

    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [ForeignKey("User")] public int CreatedBy { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Question> Questions { get; set; }

    public void Update(string title, string description, int createdBy)
    {
        Title = title;
        Description = description;
        CreatedBy = createdBy;
        UpdatedTime = DateTime.UtcNow;
    }
}