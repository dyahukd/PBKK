namespace StudentRegistration.Models
{
    public class Mahasiswa
    {
        public int Id { get; set; }
        public string NIM { get; set; } = "";
        public string Nama { get; set; } = "";
        public string Prodi { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Alamat { get; set; } = "";
        public string NoTelepon { get; set; } = "";
    }
}
