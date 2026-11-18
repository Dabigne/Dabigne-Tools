using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Models.Dice;

namespace Module.WarhammerTools.ViewModels;

public partial class DiceRollViewModel : ObservableObject
{
    [ObservableProperty]
    private DiceRollRequest? _currentRollRequest;

    [ObservableProperty]
    private string _rollResultText = "Build a dice pool or quick-roll";

    [ObservableProperty]
    private int _lastRollTotal;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PoolFormula))]
    private int _modifier;

    public ObservableCollection<DicePoolItemViewModel> PoolItems { get; } = new();
    public ObservableCollection<string> RollHistory { get; } = new();

    public string PoolFormula
    {
        get
        {
            var parts = PoolItems
                .Where(p => p.Count > 0)
                .Select(p => $"{p.Count}{p.Label}")
                .ToList();

            if (parts.Count == 0)
                return "Empty pool (click + to add dice)";

            string formula = string.Join(" + ", parts);
            if (Modifier > 0) formula += $" + {Modifier}";
            else if (Modifier < 0) formula += $" - {Math.Abs(Modifier)}";

            return formula;
        }
    }

    public bool CanRollPool => PoolItems.Any(p => p.Count > 0);

    public DiceRollViewModel()
    {
        var items = new[]
        {
            new DicePoolItemViewModel(DieType.D4, "d4"),
            new DicePoolItemViewModel(DieType.D6, "d6"),
            new DicePoolItemViewModel(DieType.D8, "d8"),
            new DicePoolItemViewModel(DieType.D10, "d10"),
            new DicePoolItemViewModel(DieType.D12, "d12"),
            new DicePoolItemViewModel(DieType.D20, "d20")
        };

        foreach (var item in items)
        {
            item.PropertyChanged += OnPoolItemChanged;
            PoolItems.Add(item);
        }
    }

    private void OnPoolItemChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DicePoolItemViewModel.Count))
        {
            OnPropertyChanged(nameof(PoolFormula));
            RollPoolCommand.NotifyCanExecuteChanged();
        }
    }

    [RelayCommand(CanExecute = nameof(CanRollPool))]
    private void RollPool()
    {
        var diceList = new List<DieType>();
        foreach (var item in PoolItems)
        {
            for (int i = 0; i < item.Count; i++)
                diceList.Add(item.Type);
        }

        CurrentRollRequest = new DiceRollRequest(diceList.ToArray());
    }

    [RelayCommand]
    private void QuickRoll(DieType dieType)
    {
        CurrentRollRequest = new DiceRollRequest(new[] { dieType });
    }

    [RelayCommand]
    private void RollD100()
    {
        CurrentRollRequest = new DiceRollRequest(Array.Empty<DieType>(), IsD100: true);
    }

    [RelayCommand]
    private void ClearPool()
    {
        foreach (var item in PoolItems)
            item.Count = 0;
        Modifier = 0;
    }

    [RelayCommand]
    private void IncrementModifier() => Modifier++;

    [RelayCommand]
    private void DecrementModifier() => Modifier--;

    [RelayCommand]
    private void OnRollCompleted(int rawTotal)
    {
        // Apply flat modifier if rolling a pool (non-d100)
        int finalTotal = rawTotal + Modifier;
        string display = Modifier != 0
            ? $"{RollResultText}  |  Mod {(Modifier > 0 ? $"+{Modifier}" : Modifier)}  ⇒  Final: {finalTotal}"
            : RollResultText;

        RollResultText = display;
        RollHistory.Insert(0, display);
    }
}