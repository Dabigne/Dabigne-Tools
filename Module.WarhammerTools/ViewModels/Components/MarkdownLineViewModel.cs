using CommunityToolkit.Mvvm.ComponentModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class MarkdownLineViewModel(string text) : ObservableObject
{
    [ObservableProperty]
    private string _text = text;

    [ObservableProperty]
    private string _draftText = text;

    [ObservableProperty]
    private bool _isEditing;

    public bool IsPreviewing => !IsEditing;

    partial void OnIsEditingChanged(bool value)
    {
        OnPropertyChanged(nameof(IsPreviewing));
    }
}
