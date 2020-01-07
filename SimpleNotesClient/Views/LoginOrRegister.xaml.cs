using System.Windows;
using SimpleNotesClient.Managers;
using SimpleNotesClient.ViewModels;

namespace SimpleNotesClient.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowsManager.SetMainWindow(this);

            DataContext = new LoginOrRegisterViewModel();

            InitializeComponent();
        }
    }
}
