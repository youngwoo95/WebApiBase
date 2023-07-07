using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Database
{
    // DbContext Class
    public class NoticeAppDbContext : DbContext
    {
        public NoticeAppDbContext()
        {
            // Empty
        }

        public NoticeAppDbContext(DbContextOptions<NoticeAppDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=123.2.156.21,1433;Database=NoticeApp;User Id=sa1;Password=wegg2650;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notice>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        public DbSet<Notice> Notices { get; set; }

    }
}
