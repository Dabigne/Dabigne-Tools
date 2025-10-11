using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterSkillViewModel : ObservableObject, IViewModel<CharacterSkill>
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;
    
    [ObservableProperty]
    private int _level = 1;

    public void SetModel(CharacterSkill model)
    {
        Name = model.Name;
        Description = model.Description;
        Level = model.Level;
    }

    public CharacterSkill GetModel()
    {
        return new CharacterSkill
        {
            Name = _name,
            Description = _description,
            Level = _level
        };
    }
}