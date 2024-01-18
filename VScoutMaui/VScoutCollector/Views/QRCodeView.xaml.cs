using VScoutCollector.ViewModels;

namespace VScoutCollector.Views;

public partial class QRCodeView : ContentPage
{
	public QRCodeView(QRCodeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}