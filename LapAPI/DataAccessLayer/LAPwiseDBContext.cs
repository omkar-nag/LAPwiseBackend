using LapAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LapAPI.DataAccessLayer
{
    public class LAPwiseDBContext : DbContext
    {
        public LAPwiseDBContext(DbContextOptions<LAPwiseDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
