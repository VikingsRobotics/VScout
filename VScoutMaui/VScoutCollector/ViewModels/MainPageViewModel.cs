using CommunityToolkit.Mvvm.Input;
using VScoutCollector.Views;

namespace VScoutCollector.ViewModels
{
    public partial class MainPageViewModel
    {
        [RelayCommand]
        private async void OpenDownloadSchedule()
        {
            await Shell.Current.GoToAsync(nameof(ConfigurationView));
        }

        [RelayCommand]
        private async void OpenRounds()
        {
            await Shell.Current.GoToAsync(nameof(ScheduleView));
        }
    }
}
