using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterAmbitionsViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _shortTerm = string.Empty;
    
    [ObservableProperty]
    private string _longTerm = string.Empty;

    public void SetModel(CharacterAmbitions model)
    {
        ShortTerm = model.ShortTerm;
        LongTerm = model.LongTerm;
    }

    public CharacterAmbitions GetModel()
    {
        return new CharacterAmbitions
        {
            ShortTerm = ShortTerm,
            LongTerm = LongTerm
        };
    }
}