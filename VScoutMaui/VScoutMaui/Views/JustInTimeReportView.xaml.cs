using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using VScoutCentral.ViewModels;

namespace VScoutCentral.Views;

public partial class JustInTimeReportView : ContentPage
{
    private readonly JustInTimeReportViewModel _viewModel;

    public JustInTimeReportView(JustInTimeReportViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    private async void OnSavedClicked(object sender, EventArgs e)
    {
        PermissionStatus status = await Permissions.RequestAsync<Permissions.StorageWrite>();

        if (status == PermissionStatus.Denied)
        {
            await Toast.Make("User doesn't have permission to access file system.").Show();
            return;
        }

        string fileLocation = string.Empty;
        try
        {
            if (!Screenshot.Default.IsCaptureSupported) return;

            IScreenshotResult screen = await VerticalStackLayout.CaptureAsync();
            using Stream imageStream = new MemoryStream();
            await screen.CopyToAsync(imageStream);
            fileLocation = await FileSaver.Default.SaveAsync($"Match{_viewModel.MatchNumber}.png", imageStream, CancellationToken.None);
            await Toast.Make($"File is saved: {fileLocation}").Show();
        }
        catch
        {
            await Toast.Make($"Done: {fileLocation}").Show();
        }
    }
}