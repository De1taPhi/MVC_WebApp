
using Microsoft.EntityFrameworkCore;
using MVC_WebApp.Models;

namespace MVC_WebApp.DB
{
    public class MainDbContext: DbContext
    {
        public MainDbContext() : base()
        {
            
        }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentDetail> DocumentDetails { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .HasIndex(u => u.Number)
                .IsUnique();
        }

    }
}
