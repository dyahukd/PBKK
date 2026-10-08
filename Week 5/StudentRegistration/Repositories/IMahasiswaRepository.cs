using StudentRegistration.Models;

namespace StudentRegistration.Repositories
{
    public interface IMahasiswaRepository
    {
        List<Mahasiswa> GetAll();
        Mahasiswa? GetById(int id);
        void Add(Mahasiswa mahasiswa);
        void Update(Mahasiswa mahasiswa);
        void Delete(Mahasiswa mahasiswa);
        bool NIMExists(string nim);
        bool NIMExists(string nim, int exceptId);
    }
}
