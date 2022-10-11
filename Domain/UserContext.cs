using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>()
              .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
               .Property(x => x.Name)
               .HasMaxLength(50)
               .IsRequired();

            modelBuilder.Entity<User>()
               .Property(x => x.Password)
               .IsRequired();

            modelBuilder.Entity<User>()
              .Property(x => x.Email)
              .HasMaxLength(25)
              .IsRequired();                     

            #endregion
        }
    }
}
