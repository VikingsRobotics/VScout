using VScoutCentral.ViewModels;

namespace VScoutCentral.Views;

public partial class ConfigurationView : ContentPage
{
	public ConfigurationView(ConfigurationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}