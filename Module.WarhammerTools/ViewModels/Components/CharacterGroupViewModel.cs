using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterGroupViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _shortTerm;

    [ObservableProperty]
    private string _longTerm;
    
    [ObservableProperty]
    private string _members;

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