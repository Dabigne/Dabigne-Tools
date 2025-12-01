using System.Windows.Input;
using Application.Core.Interfaces.Types;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;

namespace Application.Core.Interfaces.Services;

public interface INavigationItem
{
    string Title { get; }

    string Icon { get; }

    Type? Type { get;}
    
    IList<INavigationItem> Children { get; }
}

public interface INavigationViewTabItem
{
    string Header { get; set; }
    
    string Class { get; set; }
    
    ICommand CloseCommand { get; set; }
    
    object Content { get; set; }
}

public interface INavigationService
{
    Type? PageType { get; }
    
    void Init();
    
    IList<INavigationItem> GetNavigationItems();
    
    INavigationViewTabItem? NavigateTo(Type pageType, string? pageParameter = null);
}