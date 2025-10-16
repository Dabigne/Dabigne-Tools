using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterSpellViewModel : ObservableObject, IViewModel<CharacterSpell>
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private int _level;
    
    [ObservableProperty]
    private string _range = string.Empty;
    
    [ObservableProperty]
    private string _target = string.Empty;
    
    [ObservableProperty]
    private string _duration = string.Empty;
    
    [ObservableProperty]
    private string _effects = string.Empty;

    public CharacterSpell GetModel()
    {
        return new CharacterSpell
        {
            Name = Name,
            Level = Level,
            Range = Range,
            Target = Target,
            Duration = Duration,
            Effects = Effects
        };
    }

    public void SetModel(CharacterSpell model)
    {
        Name = model.Name;
        Level = model.Level;
        Range = model.Range;
        Target = model.Target;
        Duration = model.Duration;
        Effects = model.Effects;
    }
}