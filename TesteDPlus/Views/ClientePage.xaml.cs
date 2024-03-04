using TesteDPlus.ViewModels;
namespace TesteDPlus.Views;

public partial class ClientePage : ContentPage
{
	public ClientePage(ClienteViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
