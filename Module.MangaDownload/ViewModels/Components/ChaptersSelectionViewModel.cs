using CommunityToolkit.Mvvm.ComponentModel;

namespace Module.MangaDownload.ViewModels.Components;

public partial class ChaptersSelectionViewModel :  ObservableObject
{
	[ObservableProperty] 
	private int _firstChapter = 1;
    
	[ObservableProperty] 
	private int _lastChapter = 1;

	partial void OnFirstChapterChanged(int value)
	{
		if (value <= LastChapter)
			return;
        
		LastChapter = value;    
	}

	partial void OnLastChapterChanged(int value)
	{
		if (value >= FirstChapter)
			return;
		FirstChapter = value;
	}

}