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
        MaxDestinyPoints = model.MaxDestinyPoints > 0 ? model.MaxDestinyPoints : 3;
        MaxChancePoints = model.MaxChancePoints > 0 ? model.MaxChancePoints : 3;
        DestinyPoints = model.DestinyPoints;
        ChancePoints = model.ChancePoints;
    }

    public CharacterDestiny GetModel()
    {
        return new CharacterDestiny
        {
            MaxDestinyPoints = MaxDestinyPoints,
            MaxChancePoints = MaxChancePoints,
            DestinyPoints = DestinyPoints,
            ChancePoints = ChancePoints,
        };
    }
}