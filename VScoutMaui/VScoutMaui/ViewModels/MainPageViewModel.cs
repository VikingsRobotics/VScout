using CommunityToolkit.Mvvm.Input;
using VScoutCentral.Services;
using VScoutCentral.Utilities;
using VScoutCentral.Views;

namespace VScoutCentral.ViewModels
{
    public partial class MainPageViewModel
    {
        private readonly IReportService _reportService;

        public MainPageViewModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        [RelayCommand]
        private async void OpenConfiguration()
        {
            await Shell.Current.GoToAsync(nameof(ConfigurationView));
        }

        [RelayCommand]
        private async void OpenRounds()
        {
            await Shell.Current.GoToAsync(nameof(ScheduleView));
        }

        [RelayCommand]
        private async void OpenReadQrCodeView()
        {
            await Shell.Current.GoToAsync(nameof(ReadQRCodeView));
        }

        [RelayCommand]
        private async void SaveDataCsv()
        {
            try
            {
                string roundDataString = await _reportService.GetAllRoundsDataCsv();
                await FileHelper.SaveFileToUserSelectedFileAsync("roundData.csv", roundDataString);
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
    }
}
