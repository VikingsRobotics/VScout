using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using VScoutCollector.Services;
using VScoutCollector.ViewModels;
using VScoutCollector.Views;
using VScoutCommon.Clients.FrcClient;
using ZXing.Net.Maui;

namespace VScoutCollector
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseBarcodeReader()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Services
            builder.Services.AddSingleton<IScheduleDatabaseService, ScheduleDatabaseService>();
            builder.Services.AddSingleton<IDatabaseSchemaService, DatabaseSchemaService>();
            builder.Services.AddSingleton<IFrcFileService, FrcFileService>();

            // Pages
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddSingleton<ConfigurationView>();
            builder.Services.AddSingleton<ConfigurationViewModel>();

            builder.Services.AddSingleton<ScheduleView>();
            builder.Services.AddSingleton<ScheduleViewModel>();

            builder.Services.AddSingleton<ScoutFormView>();
            builder.Services.AddSingleton<ScoutFormViewModel>();

            builder.Services.AddSingleton<QRCodeView>();
            builder.Services.AddSingleton<QRCodeViewModel>();

            // Clients
            builder.Services.AddFrcClient(options =>
            {
                options.Year = 2022;
                options.EventCode = "MNDU2";

            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}