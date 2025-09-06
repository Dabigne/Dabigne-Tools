using Application.Core.Interfaces.Services;
using Application.ViewModels;
using Avalonia.Controls;

namespace Application.Views;

public partial class MainWindow : Window
{
    public MainWindow(IInstanceProvider instanceProvider)
    //MainWindowViewModel viewModel, INavigationService  navigationService)
    {
        InitializeComponent();

        var navigationService = instanceProvider.GetInstance<INavigationService>();
        navigationService.Init(ContentPresenter);

        DataContext = instanceProvider.GetInstance<MainWindowViewModel>();
    }
}