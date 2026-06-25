using Application.Core.Interfaces.Services;
using Avalonia.Controls;
using Dabigne.Tools.ViewModels;

namespace Dabigne.Tools.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel => (DataContext as MainWindowViewModel)!;
    
    public MainWindow(
	    IFileService fileService, 
	    MainWindowViewModel viewModel)
    {
        InitializeComponent();

        fileService.SetWindow(this);
        
        DataContext = viewModel;

        Closing += (sender, args) => ViewModel.Close();
    }
}