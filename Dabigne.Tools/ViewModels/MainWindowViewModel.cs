using System.Collections.Generic;
using System.Linq;
using Application.Core.Interfaces.Services;
using Application.Core.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Dabigne.Tools.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ISessionService _sessionService;

    public IList<INavigationItem> NavigationItems { get; }
    
    public OutputViewModel Output  { get; }
    
    [ObservableProperty]
    private INavigationItem? _selectedItem;
    
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
    
    public MainWindowViewModel(
        INavigationService navigationService, 
        IOutputService outputService,
        ISessionService sessionService)
    {
        _navigationService = navigationService;
        _sessionService = sessionService;

        Output = new OutputViewModel(outputService);
        NavigationItems = _navigationService.GetNavigationItems().ToList();
        
        _sessionService.LoadSession();
        SelectedItem = _navigationService.PageType != null 
            ? NavigationItems.FirstOrDefault(x => x.Type == _navigationService.PageType) 
            : NavigationItems.First();
    }

    partial void OnSelectedItemChanged(INavigationItem? value)
    {
        if (SelectedItem?.Type == null || SelectedItem?.Type == _navigationService.PageType)
            return;
        
        _navigationService.NavigateTo(SelectedItem.Type);
    }

    public void Close()
    {
        _sessionService.SaveSession();
    }
}