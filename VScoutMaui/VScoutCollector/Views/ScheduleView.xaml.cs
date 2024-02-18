using VScoutCollector.ViewModels;

namespace VScoutCollector.Views;

public partial class ScheduleView : ContentPage
{
    private readonly ScheduleViewModel _viewModel;

    public ScheduleView(ScheduleViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
        NavigatedTo += ScheduleView_NavigatedTo;
    }

    private void ScheduleView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        RoundsCollectionView.SelectedItems.Clear();
    }

    private async void OnViewQrCodeClicked(object sender, EventArgs e)
    {
        List<RoundViewModel> roundViewModels = RoundsCollectionView.SelectedItems.Cast<RoundViewModel>().ToList();

        await _viewModel.ShowQRCodeAsync(roundViewModels.Select(round => round.MatchNumber).ToList());
    }
}