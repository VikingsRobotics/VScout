using System.Windows;
using VScoutDataCollector.ViewModels;
using VScoutDataCollector.Views;

namespace VScoutDataCollector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.NavigationFrame.Navigate(new ScheduleView());
        }
    }
}
