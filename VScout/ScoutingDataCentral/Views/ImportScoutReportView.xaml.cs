using ScoutingDataCentral.ViewModels;
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

namespace ScoutingDataCentral.Views
{
    /// <summary>
    /// Interaction logic for ImportScoutReportView.xaml
    /// </summary>
    public partial class ImportScoutReportView : Page
    {
        ImportScoutReportViewModel viewModel = new ImportScoutReportViewModel();

        /// <summary>
        /// Creates a new instance of ImportScoutReportView.
        /// </summary>
        public ImportScoutReportView(string scheduleFilePath)
        {
            InitializeComponent();
            this.DataContext = viewModel;

            this.viewModel.LoadSchedule(scheduleFilePath);
        }

        private void OnImportButtonClick(object sender, RoutedEventArgs e)
        {
            this.viewModel.SyncFiles();
        }

        private void DataGridCell_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
