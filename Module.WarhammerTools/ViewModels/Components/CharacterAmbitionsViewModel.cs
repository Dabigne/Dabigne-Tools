using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterAmbitionsViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _shortTerm;
    
    [ObservableProperty]
    private string _longTerm;

    public void SetModel(CharacterAmbitions model)
    {
        ShortTerm = model.ShortTerm;
        LongTerm = model.LongTerm;
    }

    public CharacterAmbitions GetModel()
    {
        return new CharacterAmbitions
        {
            ShortTerm = _shortTerm,
            LongTerm = _longTerm
        };
    }
}