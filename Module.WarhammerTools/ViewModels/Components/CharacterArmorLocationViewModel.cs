using CommunityToolkit.Mvvm.ComponentModel;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterArmorLocationViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _hitScore;

    [ObservableProperty]
    private int _value;

    [ObservableProperty] 
    private string _title;
}