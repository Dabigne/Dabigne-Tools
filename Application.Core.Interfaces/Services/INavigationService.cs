using Application.Core.Interfaces.Types;
using Avalonia.Controls.Presenters;

namespace Application.Core.Interfaces.Services;

public interface INavigationItem
{
    string Title { get; }

    string Icon { get; }

    Type? Type { get;}
    
    IList<INavigationItem> Children { get; }
}

public interface INavigationService
{
    void Init(ContentPresenter presenter);
    
    IList<INavigationItem> GetNavigationItems();
    
    void NavigateTo(Type pageType);
}