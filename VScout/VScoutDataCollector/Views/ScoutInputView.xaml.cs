using VScoutDataCollector.ViewModels;
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

namespace VScoutDataCollector.Views
{
    /// <summary>
    /// Interaction logic for ScoutInputView.xaml
    /// </summary>
    public partial class ScoutInputView : Page
    {
        ScoutInputViewModel scoutInputViewModel = new ScoutInputViewModel();
        public ScoutInputView(int teamNumber, int roundNumber)
        {
            InitializeComponent();
            this.DataContext = scoutInputViewModel;
            this.scoutInputViewModel.TeamNumber = teamNumber;
            this.scoutInputViewModel.RoundNumber = roundNumber;
            this.scoutInputViewModel.Open();
        }

        #region Hatches
        private void OnHatchCargoShipPlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.CargoShipHatchCount++;
        }

        private void OnHatchCargoShipMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.CargoShipHatchCount--;
        }

        private void OnHatchLevelOnePlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelOneHatchCount++;
        }

        private void OnHatchLevelOneMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelOneHatchCount--;
        }

        private void OnHatchLevelTwoPlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelTwoHatchCount++;
        }

        private void OnHatchLevelTwoMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelTwoHatchCount--;
        }

        private void OnHatchLevelThreePlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelThreeHatchCount++;
        }

        private void OnHatchLevelThreeMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelThreeHatchCount--;
        }
        #endregion

        #region Cargo
        private void OnCargoCargoShipPlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.CargoShipCargoCount++;
        }

        private void OnCargoCargoShipMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.CargoShipCargoCount--;
        }

        private void OnCargoLevelOnePlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelOneCargoCount++;
        }

        private void OnCargoLevelOneMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelOneCargoCount--;
        }

        private void OnCargoLevelTwoPlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelTwoCargoCount++;
        }

        private void OnCargoLevelTwoMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelTwoCargoCount--;
        }

        private void OnCargoLevelThreePlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelThreeCargoCount++;
        }

        private void OnCargoLevelThreeMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.LevelThreeCargoCount--;
        }
        #endregion

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel.Save();
            this.NavigationService.GoBack();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
