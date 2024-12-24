using SelectableOption.ViewModels;

namespace SelectableOption.Views;

public partial class LandingPage : ContentPage
{
	public LandingPage()
	{
		InitializeComponent();
		this.BindingContext = new LandingViewModel();
	
	}
}