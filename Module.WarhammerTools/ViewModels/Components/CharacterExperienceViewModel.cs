using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterExperienceViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    private int _current;
    
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Total))]
    private int _spent;

    public int Total => Current + Spent;

    public void SetModel(CharacterExperience model)
    {
        Current = model.Current;
        Spent = model.Spent;
    }
}