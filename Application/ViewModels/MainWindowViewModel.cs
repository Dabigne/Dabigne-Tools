using System.Collections.Generic;
using System.Linq;
using Application.Core.Interfaces.Services;
using Application.Core.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Application.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IOutputService _outputService;

    public IList<INavigationItem> NavigationItems { get; } = new List<INavigationItem>();
    
    public OutputViewModel Output  { get; }
    
    [ObservableProperty]
    private INavigationItem _selectedItem;
    
    [ObservableProperty]
    private bool _isPaneOpen = true;

    [ObservableProperty] 
    private string _paneButtonContent = "<";
    
    [RelayCommand]
    private void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
        PaneButtonContent = IsPaneOpen ? "<" : ">";
    }
    
    public MainWindowViewModel(INavigationService navigationService, IOutputService outputService)
    {
        _navigationService = navigationService;
        _outputService = outputService;
        Output = new OutputViewModel(outputService);
        NavigationItems = _navigationService.GetNavigationItems().ToList();
        SelectedItem = NavigationItems.First();
    }

    partial void OnSelectedItemChanged(INavigationItem navigationItem)
    {
        if (SelectedItem == null)
            return;
        
        _navigationService.NavigateTo(SelectedItem.Type);
    }
}