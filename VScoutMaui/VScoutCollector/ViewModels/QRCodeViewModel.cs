using CommunityToolkit.Mvvm.ComponentModel;

namespace VScoutCollector.ViewModels
{
    [QueryProperty(nameof(QrCodeValue), "qrCodeValue")]
    public partial class QRCodeViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _qrCodeValue;
    }
}
