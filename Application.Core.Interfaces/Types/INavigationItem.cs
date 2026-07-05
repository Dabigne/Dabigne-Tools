namespace Application.Core.Interfaces.Types;

public interface INavigationItem
{
	string Title { get; }

	string Icon { get; }

	Type? Type { get;}
    
	IList<INavigationItem> Children { get; }
}