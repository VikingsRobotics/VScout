using VScoutCollector.ViewModels;

namespace VScoutCollector.Views;

public partial class ScoutFormView : ContentPage
{
	public ScoutFormView(ScoutFormViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}