using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentRegistration.Models;
using StudentRegistration.Repositories;

namespace StudentRegistration.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly IMahasiswaRepository _repository;
        private Mahasiswa? _mahasiswaSedangDiedit;

        public ObservableCollection<Mahasiswa> DaftarMahasiswa { get; } = new();
        public ObservableCollection<Mahasiswa> MahasiswaTampil { get; } = new();

        public ObservableCollection<string> DaftarProdi { get; } = new()
        {
            "Teknik Informatika",
            "Sistem Informasi",
            "Teknik Komputer",
            "Teknologi Informasi",
            "Sains Data"
        };

        public ObservableCollection<string> DaftarGender { get; } = new()
        {
            "Laki-laki",
            "Perempuan"
        };

        [ObservableProperty] private string nim = "";
        [ObservableProperty] private string nama = "";
        [ObservableProperty] private string prodi = "";
        [ObservableProperty] private string gender = "";
        [ObservableProperty] private string alamat = "";
        [ObservableProperty] private string noTelepon = "";
        [ObservableProperty] private string keywordSearch = "";
        [ObservableProperty] private Mahasiswa? mahasiswaTerpilih;
        [ObservableProperty] private string formTitle = "Tambah Mahasiswa";
        [ObservableProperty] private string simpanButtonText = "Simpan";
        [ObservableProperty] private string pesan = "";

        public int TotalMahasiswa => DaftarMahasiswa.Count;
        public int TotalInformatika => DaftarMahasiswa.Count(m => m.Prodi == "Teknik Informatika");
        public int TotalSistemInformasi => DaftarMahasiswa.Count(m => m.Prodi == "Sistem Informasi");
        public int TotalLakiLaki => DaftarMahasiswa.Count(m => m.Gender == "Laki-laki");
        public int TotalPerempuan => DaftarMahasiswa.Count(m => m.Gender == "Perempuan");
        public string JumlahData => $"{MahasiswaTampil.Count} data mahasiswa";

        public MainViewModel(IMahasiswaRepository repository)
        {
            _repository = repository;
            LoadData();
        }

        partial void OnKeywordSearchChanged(string value) => RefreshData();

        [RelayCommand]
        private void Simpan()
        {
            string nimInput = Nim.Trim();
            string namaInput = Nama.Trim();
            string prodiInput = Prodi?.Trim() ?? "";
            string genderInput = Gender?.Trim() ?? "";
            string alamatInput = Alamat.Trim();
            string telpInput = NoTelepon.Trim();

            if (string.IsNullOrWhiteSpace(nimInput))
            {
                Pesan = "NIM harus diisi.";
                return;
            }

            if (!nimInput.All(char.IsDigit) || nimInput.Length < 8 || nimInput.Length > 12)
            {
                Pesan = "NIM harus berupa angka 8-12 digit.";
                return;
            }

            if (string.IsNullOrWhiteSpace(namaInput) || namaInput.Length < 3)
            {
                Pesan = "Nama minimal 3 karakter.";
                return;
            }

            if (string.IsNullOrWhiteSpace(prodiInput))
            {
                Pesan = "Program studi harus dipilih.";
                return;
            }

            if (string.IsNullOrWhiteSpace(genderInput))
            {
                Pesan = "Jenis kelamin harus dipilih.";
                return;
            }

            if (string.IsNullOrWhiteSpace(alamatInput) || alamatInput.Length < 5)
            {
                Pesan = "Alamat minimal 5 karakter.";
                return;
            }

            if (string.IsNullOrWhiteSpace(telpInput) ||
                (!telpInput.StartsWith("08") && !telpInput.StartsWith("628")))
            {
                Pesan = "Nomor telepon harus diawali 08 atau 628.";
                return;
            }

            if (!telpInput.All(char.IsDigit))
            {
                Pesan = "Nomor telepon hanya boleh berisi angka.";
                return;
            }

            if (_mahasiswaSedangDiedit == null)
            {
                if (_repository.NIMExists(nimInput))
                {
                    Pesan = "NIM tersebut sudah terdaftar.";
                    return;
                }

                var baru = new Mahasiswa
                {
                    NIM = nimInput,
                    Nama = namaInput,
                    Prodi = prodiInput,
                    Gender = genderInput,
                    Alamat = alamatInput,
                    NoTelepon = telpInput
                };
                _repository.Add(baru);
                Pesan = "Data berhasil ditambahkan.";
            }
            else
            {
                if (_repository.NIMExists(nimInput, _mahasiswaSedangDiedit.Id))
                {
                    Pesan = "NIM sudah digunakan mahasiswa lain.";
                    return;
                }

                _mahasiswaSedangDiedit.NIM = nimInput;
                _mahasiswaSedangDiedit.Nama = namaInput;
                _mahasiswaSedangDiedit.Prodi = prodiInput;
                _mahasiswaSedangDiedit.Gender = genderInput;
                _mahasiswaSedangDiedit.Alamat = alamatInput;
                _mahasiswaSedangDiedit.NoTelepon = telpInput;
                _repository.Update(_mahasiswaSedangDiedit);
                Pesan = "Data berhasil diperbarui.";
            }

            LoadData();
            ResetForm();
        }

        [RelayCommand]
        private void Edit()
        {
            if (MahasiswaTerpilih == null)
            {
                Pesan = "Pilih mahasiswa yang ingin diedit.";
                return;
            }

            _mahasiswaSedangDiedit = MahasiswaTerpilih;
            Nim = _mahasiswaSedangDiedit.NIM;
            Nama = _mahasiswaSedangDiedit.Nama;
            Prodi = _mahasiswaSedangDiedit.Prodi;
            Gender = _mahasiswaSedangDiedit.Gender;
            Alamat = _mahasiswaSedangDiedit.Alamat;
            NoTelepon = _mahasiswaSedangDiedit.NoTelepon;

            FormTitle = "Edit Mahasiswa";
            SimpanButtonText = "Simpan Perubahan";
            Pesan = "";
        }

        [RelayCommand]
        private void Hapus()
        {
            if (MahasiswaTerpilih == null)
            {
                Pesan = "Pilih mahasiswa yang ingin dihapus.";
                return;
            }

            var konfirmasi = MessageBox.Show(
                $"Yakin ingin menghapus data \"{MahasiswaTerpilih.Nama}\"?",
                "Konfirmasi Hapus",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (konfirmasi != MessageBoxResult.Yes) return;

            _repository.Delete(MahasiswaTerpilih);
            MahasiswaTerpilih = null;
            LoadData();
            Pesan = "Data berhasil dihapus.";
        }

        [RelayCommand]
        private void Reset()
        {
            ResetForm();
            Pesan = "";
        }

        private void LoadData()
        {
            DaftarMahasiswa.Clear();
            foreach (var m in _repository.GetAll())
                DaftarMahasiswa.Add(m);

            RefreshData();
            RefreshStatistics();
        }

        private void ResetForm()
        {
            Nim = "";
            Nama = "";
            Prodi = "";
            Gender = "";
            Alamat = "";
            NoTelepon = "";
            _mahasiswaSedangDiedit = null;
            FormTitle = "Tambah Mahasiswa";
            SimpanButtonText = "Simpan";
        }

        private void RefreshData()
        {
            MahasiswaTampil.Clear();
            string keyword = KeywordSearch.Trim().ToLowerInvariant();

            var hasil = DaftarMahasiswa.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                hasil = hasil.Where(m =>
                    m.NIM.ToLowerInvariant().Contains(keyword) ||
                    m.Nama.ToLowerInvariant().Contains(keyword) ||
                    m.Prodi.ToLowerInvariant().Contains(keyword));
            }

            foreach (var m in hasil)
                MahasiswaTampil.Add(m);

            OnPropertyChanged(nameof(JumlahData));
        }

        private void RefreshStatistics()
        {
            OnPropertyChanged(nameof(TotalMahasiswa));
            OnPropertyChanged(nameof(TotalInformatika));
            OnPropertyChanged(nameof(TotalSistemInformasi));
            OnPropertyChanged(nameof(TotalLakiLaki));
            OnPropertyChanged(nameof(TotalPerempuan));
            OnPropertyChanged(nameof(JumlahData));
        }
    }
}
