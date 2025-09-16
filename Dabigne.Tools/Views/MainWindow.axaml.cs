using Application.Core.Interfaces.Services;
using Avalonia.Controls;
using Dabigne.Tools.ViewModels;

namespace Dabigne.Tools.Views;

public partial class MainWindow : Window
{
    public MainWindow(IInstanceProvider instanceProvider)
    {
        InitializeComponent();

        var fileService = instanceProvider.GetInstance<IFileService>();
        fileService.SetWindow(this);
        
        var navigationService = instanceProvider.GetInstance<INavigationService>();
        navigationService.Init(ContentPresenter);

        DataContext = instanceProvider.GetInstance<MainWindowViewModel>();
    }
}