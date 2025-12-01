using Application.Core.Interfaces.Services;
using Avalonia.Controls;
using Dabigne.Tools.ViewModels;

namespace Dabigne.Tools.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;
    
    public MainWindow(IInstanceProvider instanceProvider)
    {
        InitializeComponent();

        var fileService = instanceProvider.GetInstance<IFileService>();
        fileService.SetWindow(this);
        
        DataContext = instanceProvider.GetInstance<MainWindowViewModel>();

        Closing += (sender, args) => ViewModel.Close();
    }
}