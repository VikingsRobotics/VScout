using CommunityToolkit.Mvvm.ComponentModel;

namespace VScoutCentral.ViewModels
{
    public partial class JustInTimeRectangeViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _station;

        [ObservableProperty]
        private int _teamNumber;

        [ObservableProperty]
        private int _rank;

        [ObservableProperty]
        private int _roundsPlayed;

        [ObservableProperty]
        private decimal _averageHighCones;

        [ObservableProperty]
        private decimal _averageMiddleCones;

        [ObservableProperty]
        private decimal _averageLowCones;

        [ObservableProperty]
        private decimal _averageHighCubes;

        [ObservableProperty]
        private decimal _averageMiddleCubes;

        [ObservableProperty]
        private decimal _averageLowCubes;

        [ObservableProperty]
        private string _comments;
    }
}
