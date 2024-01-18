using VScoutCollector.ViewModels;

namespace VScoutCollector.Views;

public partial class ConfigurationView : ContentPage
{
	public ConfigurationView(ConfigurationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}