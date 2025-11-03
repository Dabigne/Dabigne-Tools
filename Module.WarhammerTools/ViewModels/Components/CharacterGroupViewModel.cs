using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterGroupViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _shortTerm = string.Empty;

    [ObservableProperty]
    private string _longTerm = string.Empty;
    
    [ObservableProperty]
    private string _members = string.Empty;

    public void SetModel(CharacterGroup model)
    {
        Name = model.Name;
        ShortTerm = model.ShortTerm;
        LongTerm = model.LongTerm;
        Members = model.Members;
    }

    public CharacterGroup GetModel()
    {
        return new CharacterGroup
        {
            Name = Name,
            ShortTerm = ShortTerm,
            LongTerm = LongTerm,
            Members = Members
        };
    }
}