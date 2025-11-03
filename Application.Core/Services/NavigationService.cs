using System.Reflection;
using Application.Core.Attributes;
using Application.Core.Interfaces.Services;
using Application.Core.Interfaces.Types;
using Avalonia.Controls.Presenters;

namespace Application.Core.Services;

public sealed class NavigationItem(string title, string icon, Type? pageType) : INavigationItem
{
    public string Title { get; set; } = title;

    public string Icon { get; set; } = icon;

    public Type? Type { get; set; } = pageType;

    public IList<INavigationItem> Children { get; } = [];
}

public class NavigationService: INavigationService
{
    private readonly IInstanceProvider _instanceProvider;
    private ContentPresenter? _contentPresenter;
    
    private List<INavigationItem> _navigationItems = [];
    
    public NavigationService(IInstanceProvider instanceProvider)
    {
        _instanceProvider = instanceProvider;
    }

    public void Init(ContentPresenter  presenter)
    {
        _contentPresenter = presenter;
        BuildNavigationItem();
    }

    private void BuildNavigationItem()
    {
        var navigatables = _instanceProvider.GetRegisteredTypes<INavigatable>();
        foreach (var navigatable in navigatables)
        {
            var attribute = navigatable.GetTypeInfo().GetCustomAttribute<NavigatableAttribute>();
            if (attribute == null)
                continue;

            var item = new NavigationItem(attribute.Title, attribute.Icon, navigatable);
            if (!string.IsNullOrEmpty(attribute.Folder))
            {
                var folderItem = _navigationItems.Find(n => n.Title == attribute.Folder);
                if (folderItem == null)
                {
                    folderItem = new NavigationItem(attribute.Folder, "Folder", null);
                    _navigationItems.Add(folderItem);
                }
                folderItem.Children.Add(item);
            }
            else
                _navigationItems.Add(item);
        }
    }

    public IList<INavigationItem> GetNavigationItems()
    {
        return _navigationItems;
    }
    
    public void NavigateTo(Type pageType)
    {
        if (_contentPresenter == null)
            return;
        
        var control = _instanceProvider.GetInstance(pageType);
        _contentPresenter.Content = control;
    }
}