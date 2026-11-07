using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.ViewModels;

namespace WarhammerCompanion;

public partial class MainWindowViewModel : ObservableObject
{
	[ObservableProperty]
	private string _title = "Warhammer Companion";
	
	[ObservableProperty]
	private CharacterSheetViewModel _characterSheet;
	
	public MainWindowViewModel(CharacterSheetViewModel characterSheet)
	{
		_characterSheet = characterSheet;
	}
	
	public void Close()
	{
	}
}