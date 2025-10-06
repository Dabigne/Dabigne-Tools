using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterResilienceViewModel : ObservableObject
{
    [ObservableProperty]
    private int _resilience;

    [ObservableProperty]
    private int _determination;

    [ObservableProperty]
    private int _motivation;

    public void SetModel(CharacterResilience model)
    {
        Resilience  = model.Resilience;
        Determination = model.Determination;
        Motivation = model.Motivation;
    }

    public CharacterResilience GetModel()
    {
        return new CharacterResilience
        {
            Resilience = Resilience,
            Determination = Determination,
            Motivation = Motivation
        };
    }
}