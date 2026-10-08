using Microsoft.EntityFrameworkCore;
using StudentRegistration.Models;

namespace StudentRegistration.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Mahasiswa> Mahasiswa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=student.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mahasiswa>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.NIM).IsRequired();
                entity.Property(m => m.Nama).IsRequired();
                entity.Property(m => m.Prodi).IsRequired();
                entity.HasIndex(m => m.NIM).IsUnique();
            });
        }
    }
}
