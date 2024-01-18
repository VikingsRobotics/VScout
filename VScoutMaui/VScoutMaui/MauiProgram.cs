using CommunityToolkit.Maui;
using VScoutCentral.Services;
using VScoutCentral.ViewModels;
using VScoutCentral.Views;
using VScoutCommon.Clients.BlueAllianceClient;
using VScoutCommon.Clients.FrcClient;
using ZXing.Net.Maui;

namespace VScoutCentral;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        builder.Services.AddSingleton<IScheduleDatabaseService, ScheduleDatabaseService>();
        builder.Services.AddSingleton<IDatabaseSchemaService, DatabaseSchemaService>();
        builder.Services.AddSingleton<IReportService, ReportService>();

        // Pages
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddSingleton<ConfigurationView>();
        builder.Services.AddSingleton<ConfigurationViewModel>();

        builder.Services.AddSingleton<ScheduleView>();
        builder.Services.AddSingleton<ScheduleViewModel>();

        builder.Services.AddSingleton<ReadQRCodeView>();
        builder.Services.AddSingleton<ReadQRCodeViewModel>();

        builder.Services.AddSingleton<JustInTimeReportView>();
        builder.Services.AddSingleton<JustInTimeReportViewModel>();
        builder.Services.AddSingleton<JustInTimeRectangeViewModel>();

        // Clients
        builder.Services.AddFrcClient(options =>
        {
            options.Year = 2023;
            options.EventCode = "MNDU2";

        });

        builder.Services.AddBlueAllianceClient(options =>
        {
            options.Year = 2023;
            options.EventCode = "MNDU2";
        });

        return builder.Build();
    }
}
