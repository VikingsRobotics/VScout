using VScoutDataCollector.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace VScoutDataCollector.Views
{
    /// <summary>
    /// Interaction logic for ScheduleView.xaml
    /// </summary>
    public partial class ScheduleView : Page
    {
        ScheduleViewModel viewModel = new ScheduleViewModel();

        public ScheduleView()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.viewModel.OpenFile();
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.viewModel.Status = "";
            DataGridCell dataGridCell = (DataGridCell)sender;
            if (dataGridCell.Column.DisplayIndex == 0) return;
            RoundViewModel viewModel = (RoundViewModel)dataGridCell.DataContext;
            int selectedTeamNumber = int.Parse(((TextBlock)dataGridCell.Content).Text);
            ClearPageHistory();
            this.NavigationService.Navigate(new ScoutInputView2022(selectedTeamNumber, viewModel.MatchNumber, this.viewModel));
        }

        private void ClearPageHistory()
        {
            while (this.NavigationService.CanGoBack)
            {
                this.NavigationService.RemoveBackEntry();
            }
        }

        private void OnExportClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.viewModel.ExportData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
