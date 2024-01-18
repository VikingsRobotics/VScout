using ScoutingDataCentral.ViewModels;
using ScoutingDataCentral.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScoutingDataCentral
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
        }

        private void OnSyncClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.viewModel.SyncFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnRoundReportClick(object sender, RoutedEventArgs e)
        {
            JustInTimeReportWindowView justInTimeReportWindowView = new JustInTimeReportWindowView();
            justInTimeReportWindowView.Show();
        }

        private void OnDownloadImagesClick(object sender, RoutedEventArgs e)
        {
            this.viewModel.ImportImages();
        }

        private void OnDownloadScheduleClick(object sender, RoutedEventArgs e)
        {
            this.viewModel.SaveSchedule();
        }

        private void OnSyncWithFRCData(object sender, RoutedEventArgs e)
        {
            this.viewModel.SyncWithFRCData();
        }

        private void OnTeamsClick(object sender, RoutedEventArgs e)
        {
            TeamsView teamsView = new TeamsView();
            teamsView.Show();
        }

        private void OnExportAllClick(object sender, RoutedEventArgs e)
        {
            this.viewModel.CreateExportAllReport();
        }

        private void OnExportScheduleClick(object sender, RoutedEventArgs e)
        {
            this.viewModel.CreateScheduleExport();
        }

        private void OnExportTeamSchedule(object sender, RoutedEventArgs e)
        {
            this.viewModel.CreateTeamScheduleExport();
        }
    }
}
