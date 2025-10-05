using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.Models;

namespace Module.WarhammerTools.ViewModels.Components;

public partial class CharacterMovementViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Walk))]
    [NotifyPropertyChangedFor(nameof(Run))]
    private int _value;

    public int Walk => 2 * Value;

    public int Run => 4 * Value;

    public void SetModel(CharacterMovement model)
    {
        Value = model.Value;
    }
}