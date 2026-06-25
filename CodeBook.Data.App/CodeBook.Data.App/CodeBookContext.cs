using Microsoft.EntityFrameworkCore;
using CodeBook.Models.App;

namespace CodeBook.Data.App
{
    public class CodeBookContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=CodeBook_DB;Trusted_Connection=true;TrustedServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.ID);
                user.Property(u => u.ID).HasColumnName("User_ID").ValueGeneratedOnAdd();
                user.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                user.Property(u => u.Email).IsRequired().HasMaxLength(100);
                user.HasIndex(u => u.Email).IsUnique();
                user.Property(u => u.password).IsRequired().HasMaxLength(256);
                user.Property(u => u.BIO).HasMaxLength(1000);
                user.Property(u => u.ProfilePicURL).HasMaxLength(2000);
                user.Property(u => u.DateCreated).HasDefaultValue("GETUCTDATE()");
                user.Property(u => u.DateUpdated).HasDefaultValue("GETUCTDATE()");
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
