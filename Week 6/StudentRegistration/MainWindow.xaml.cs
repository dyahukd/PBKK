using System.Windows;
using StudentRegistration.Repositories;

namespace StudentRegistration;

public partial class MainWindow : Window
{
    private readonly StudentRepository _studentRepository = new();

    public MainWindow()
    {
        InitializeComponent();
        LoadStudents();
    }

    private void LoadStudents()
    {
        RefreshButton.IsEnabled = false;
        StatusText.Text = "Menghubungkan ke database...";

        try
        {
            StudentDataGrid.ItemsSource = _studentRepository.GetAll();
            TotalText.Text = _studentRepository.GetTotal().ToString();
            StatusText.Text = "Terhubung • Data berhasil dimuat";
            LastUpdatedText.Text = $"Terakhir diperbarui: {DateTime.Now:dd MMM yyyy, HH:mm}";
        }
        catch (Exception ex)
        {
            StatusText.Text = "Koneksi belum berhasil";
            MessageBox.Show(
                "Data belum dapat dimuat dari SQL Server.\n\n" +
                "Periksa apakah SQL Server aktif, nama instance pada connection string benar, " +
                "dan script database sudah dijalankan.\n\nDetail: " + ex.Message,
                "Database Connection Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        finally
        {
            RefreshButton.IsEnabled = true;
        }
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e) => LoadStudents();
}
