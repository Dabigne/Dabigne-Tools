using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterDestinyViewModel : ObservableObject
{
    [ObservableProperty]
    private int _maxDestinyPoints;

    [ObservableProperty]
    private int _destinyPoints;
    
    [ObservableProperty]
    private int _maxChancePoints;
    
    [ObservableProperty]
    private int _chancePoints;

    public void SetModel(CharacterDestiny model)
    {
        MaxDestinyPoints = model.MaxDestinyPoints;
        MaxChancePoints = model.MaxChancePoints;
        DestinyPoints = model.DestinyPoints;
        ChancePoints = model.ChancePoints;
    }
}