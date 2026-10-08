using System.Windows;
using StudentRegistration.Repositories;
using StudentRegistration.ViewModels;

namespace StudentRegistration.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var repository = new MahasiswaRepository();
            DataContext = new MainViewModel(repository);
        }
    }
}
