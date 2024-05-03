using Microsoft.EntityFrameworkCore;
using QuizApplication.Core.Models;

namespace QuizApplication.Data;

public class Context : DbContext
{
    public Context(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<UserQuizResult> QuizResults { get; set; }
    public DbSet<UserResponse> UserResponses { get; set; }
}