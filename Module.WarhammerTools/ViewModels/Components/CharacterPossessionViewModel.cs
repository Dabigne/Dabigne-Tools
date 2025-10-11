using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterPossessionViewModel : ObservableObject, IViewModel<CharacterPossession>
{
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private int _footprint;
    
    public CharacterPossession GetModel()
    {
        return new CharacterPossession()
        {
            Name = Name,
            Footprint = Footprint
        };
    }

    public void SetModel(CharacterPossession model)
    {
        Name = model.Name;
        Footprint = model.Footprint;
    }
}