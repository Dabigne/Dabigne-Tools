using Avalonia.Controls;
using Dabigne.Shell.ViewModels;

namespace Dabigne.Shell.Views;


public partial class MainWindow : Window
{
	public MainWindow(MainWindowViewModel viewModel)
	{
		InitializeComponent();
		DataContext = viewModel;
	}
}