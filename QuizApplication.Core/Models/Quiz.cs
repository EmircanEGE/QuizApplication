using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApplication.Core.Models;

public class Quiz : BaseEntity
{
    [Required] public string Title { get; set; }

    [Required] public string Description { get; set; }

    [ForeignKey("User")] public int CreatedBy { get; set; }

    public virtual ICollection<Question> Questions { get; set; }

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
        UpdatedTime = DateTime.UtcNow;
    }
}