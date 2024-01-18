using VScoutCentral.ViewModels;

namespace VScoutCentral.Views;

public partial class ScheduleView : ContentPage
{
	public ScheduleView(ScheduleViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}