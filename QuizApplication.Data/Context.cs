using Microsoft.EntityFrameworkCore;
using QuizApplication.Core.Models;

namespace QuizApplication.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
            
        }

        DbSet<User> Users { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Answer> Answers { get; set; }
    }
}
