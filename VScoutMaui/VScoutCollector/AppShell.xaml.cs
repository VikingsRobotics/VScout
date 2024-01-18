using VScoutCollector.Views;

namespace VScoutCollector
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ConfigurationView), typeof(ConfigurationView));
            Routing.RegisterRoute(nameof(ScheduleView), typeof(ScheduleView));
            Routing.RegisterRoute(nameof(ScoutFormView), typeof(ScoutFormView));
            Routing.RegisterRoute(nameof(QRCodeView), typeof(QRCodeView));
        }
    }
}