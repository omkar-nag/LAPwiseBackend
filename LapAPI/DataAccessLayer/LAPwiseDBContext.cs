using LapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.DataAccessLayer
{
    public class LAPwiseDBContext : DbContext
    {
        public LAPwiseDBContext(DbContextOptions<LAPwiseDBContext> options) : base(options) { }
        public DbSet<AssessmentResults> AssessmentResults { get; set; }
        public DbSet<Assessments> Assessments { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Quizzes> Quizzes { get; set; }
        public DbSet<SubTopics> SubTopics { get; set; }
        public DbSet<Topics> Topics { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
