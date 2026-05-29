using CommunityToolkit.Mvvm.ComponentModel;
using Module.PdfTools.Interfaces;

namespace Module.PdfTools.ViewModels.Components;

public partial class FontsInformationViewModel : ObservableObject
{
	[ObservableProperty]
	private IList<IFontInformation>? _fontsList;
	
	[ObservableProperty]
	private IFontInformation? _selectedFont;
	
	public void Init(IList<IFontInformation> fontsList)
	{
		FontsList = fontsList;
		if (fontsList.Count == 0)
			return;
		
		SelectedFont = fontsList[0];
	}
}