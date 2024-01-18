using ScoutingDataCentral.ViewModels;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VScoutCommon.Common;

namespace ScoutingDataCentral.Views
{
    /// <summary>
    /// Interaction logic for JustInTimeReportWindowView.xaml
    /// </summary>
    public partial class JustInTimeReportWindowView : Window
    {
        JustInTimeReportWindowViewModel viewModel = new JustInTimeReportWindowViewModel();
        public JustInTimeReportWindowView()
        {
            InitializeComponent();
            this.DataContext = viewModel;

            string scheduleFilePath = Path.Combine(Constants.CentralBaseFilePath, "Schedule.json");
            if (File.Exists(scheduleFilePath))
            {
                this.viewModel.LoadSchedule(scheduleFilePath);
            }
            else
            {
                this.viewModel.Message = $"Schedule file not found: {scheduleFilePath}.";
            }
        }

        private void DataGridRow_MouseClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow dataGridRow = (DataGridRow)sender;
            RoundViewModel viewModel = (RoundViewModel)dataGridRow.DataContext;

            this.viewModel.LoadJitTeamTiles(viewModel);
            this.viewModel.DetermineRoundWhenAllPreviousDataIsComplete(viewModel.MatchNumber);
        }

        private void OnPrintClick(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                printDialog.PrintVisual(this, "JIT Rectangles");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
