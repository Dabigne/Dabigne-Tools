using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Module.WarhammerTools.Interfaces;

public interface IListHeader
{
	string Title { get; set; }
	
	IRelayCommand AddCommand { get; }
	
	IRelayCommand RemoveCommand { get; }
}