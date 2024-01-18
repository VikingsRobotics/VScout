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
using VScoutDataCollector.ViewModels;

namespace VScoutDataCollector.Views
{
    /// <summary>
    /// Interaction logic for ScoutInputView2020.xaml
    /// </summary>
    public partial class ScoutInputView2020 : Page
    {
        ScoutInputViewModel2020 scoutInputViewModel2020 = new ScoutInputViewModel2020();
        ScheduleViewModel parentViewModel;

        public ScoutInputView2020(int teamNumber, int roundNumber, ScheduleViewModel parentViewModel)
        {
            InitializeComponent();
            this.DataContext = this.scoutInputViewModel2020;
            this.parentViewModel = parentViewModel;
            this.scoutInputViewModel2020.TeamNumber = teamNumber;
            this.scoutInputViewModel2020.RoundNumber = roundNumber;
            this.scoutInputViewModel2020.Open();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.Save();
            this.parentViewModel.UpdateHasData(this.scoutInputViewModel2020.TeamNumber, this.scoutInputViewModel2020.RoundNumber);
            this.NavigationService.GoBack();
        }

        private void OnBottomPortAutoMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.LowGoalAutonomous--;
        }

        private void OnBottomPortAutoPlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.LowGoalAutonomous++;
        }

        private void OnHighGoalAutoMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.HighGoalAutonomous--;
        }

        private void OnHighGoalAutoPlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.HighGoalAutonomous++;
        }

        private void OnLowGoalMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.LowGoal--;
        }

        private void OnLowGoalPlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.LowGoal++;
        }

        private void OnHighGoalMinusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.HighGoal--;
        }

        private void OnHighGoalPlusClick(object sender, RoutedEventArgs e)
        {
            this.scoutInputViewModel2020.HighGoal++;
        }
    }
}
