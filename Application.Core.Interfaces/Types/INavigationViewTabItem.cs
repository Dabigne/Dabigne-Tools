using System.Windows.Input;

namespace Application.Core.Interfaces.Types;

public interface INavigationViewTabItem
{
	string Header { get; set; }
	
	string Class { get; set; }
	
	ICommand CloseCommand { get; set; }
	
	object Content { get; set; }
}