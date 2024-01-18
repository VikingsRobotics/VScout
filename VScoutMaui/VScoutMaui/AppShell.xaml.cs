using VScoutCentral.Views;

namespace VScoutCentral;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ConfigurationView), typeof(ConfigurationView));
		Routing.RegisterRoute(nameof(ScheduleView), typeof(ScheduleView));
		Routing.RegisterRoute(nameof(ReadQRCodeView), typeof(ReadQRCodeView));
		Routing.RegisterRoute(nameof(JustInTimeReportView), typeof(JustInTimeReportView));
	}
}
