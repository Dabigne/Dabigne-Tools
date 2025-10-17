using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterInjuriesViewModel : ObservableObject, IViewModel<CharacterInjuries>
{
    private readonly ICharacteristicRulesService _rulesService;
    
    [ObservableProperty]
    private int _toughBonus;
    
    [ObservableProperty]
    private int _currentHealth;
    
    [ObservableProperty]
    private string _description = string.Empty;

    public int ForceBonus => _rulesService.GetBonusValue("F");
    
    public int EnduranceBonus => 2 * _rulesService.GetBonusValue("E");
    
    public int ForceMentalBonus => _rulesService.GetBonusValue("FM");
    
    public int Total => ForceBonus + EnduranceBonus + ForceMentalBonus + ToughBonus;

    public CharacterInjuriesViewModel(ICharacteristicRulesService rulesService)
    {
        _rulesService = rulesService;
    }
    
    public CharacterInjuries GetModel()
    {
        return new CharacterInjuries
        {
            ToughBonus = ToughBonus,
            CurrentHealth = CurrentHealth,
            Description = Description,
        };
    }

    public void SetModel(CharacterInjuries model)
    {
        ToughBonus = model.ToughBonus;
        CurrentHealth = model.CurrentHealth;
        Description = model.Description;
    }
}