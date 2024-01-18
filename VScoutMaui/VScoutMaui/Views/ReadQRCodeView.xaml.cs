using VScoutCentral.ViewModels;

namespace VScoutCentral.Views;

public partial class ReadQRCodeView : ContentPage
{
    private readonly ReadQRCodeViewModel _viewModel;

    public ReadQRCodeView(ReadQRCodeViewModel vm)
    {
        InitializeComponent();
        _viewModel = vm;
    }

    private async void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        string value = e.Results[0].Value;

        await _viewModel.ProcessQRCodeAsync(value);

        frame.BackgroundColor = new Color(0, 255, 0);

        await Task.Delay(2000);

        frame.BackgroundColor = new Color(255, 255, 255);
    }

    private void OnAppearing(object sender, EventArgs e)
    {
        cameraBarcodeReaderView.IsDetecting = true;
    }

    private void OnDisappearing(object sender, EventArgs e)
    {
        cameraBarcodeReaderView.IsDetecting = false;

        frame.BackgroundColor = new Color(255, 255, 255);
    }
}