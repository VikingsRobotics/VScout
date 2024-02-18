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
        private decimal _averageAmp;

        [ObservableProperty]
        private decimal _averageSpeaker;

        [ObservableProperty]
        private string _comments;
    }
}
