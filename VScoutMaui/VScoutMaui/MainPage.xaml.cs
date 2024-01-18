using VScoutCentral.Services;
using VScoutCentral.ViewModels;
using VScoutCommon.Clients.FrcClient;

namespace VScoutCentral;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

