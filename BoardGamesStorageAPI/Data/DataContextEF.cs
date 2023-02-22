using BoardGamesStorageAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesStorageAPI.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContextEF(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<BoardGame> BoardGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                      optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BoardGamesStorageSchema");

            modelBuilder.Entity<BoardGame>()
                .ToTable("BoardGames", "BoardGamesStorageSchema")
                .HasKey(b => b.BoardGameId);
        }
    }
}
