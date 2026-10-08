using Microsoft.EntityFrameworkCore;
using StudentRegistration.Data;
using StudentRegistration.Models;

namespace StudentRegistration.Repositories
{
    public class MahasiswaRepository : IMahasiswaRepository
    {
        public List<Mahasiswa> GetAll()
        {
            using var context = new AppDbContext();
            return context.Mahasiswa
                .AsNoTracking()
                .OrderBy(m => m.NIM)
                .ToList();
        }

        public Mahasiswa? GetById(int id)
        {
            using var context = new AppDbContext();
            return context.Mahasiswa
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);
        }

        public void Add(Mahasiswa mahasiswa)
        {
            using var context = new AppDbContext();
            context.Mahasiswa.Add(mahasiswa);
            context.SaveChanges();
        }

        public void Update(Mahasiswa mahasiswa)
        {
            using var context = new AppDbContext();
            context.Mahasiswa.Update(mahasiswa);
            context.SaveChanges();
        }

        public void Delete(Mahasiswa mahasiswa)
        {
            using var context = new AppDbContext();
            context.Mahasiswa.Remove(mahasiswa);
            context.SaveChanges();
        }

        public bool NIMExists(string nim)
        {
            using var context = new AppDbContext();
            return context.Mahasiswa.Any(m => m.NIM == nim);
        }

        public bool NIMExists(string nim, int exceptId)
        {
            using var context = new AppDbContext();
            return context.Mahasiswa.Any(m => m.NIM == nim && m.Id != exceptId);
        }
    }
}
