using System.Numerics;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Threading;

namespace Module.WarhammerTools.Models.Dice;

public record DiceRollRequest(DieType[] Dice, bool IsD100 = false);

public class RpgDiceTrayControl : Control
{
    private readonly DiceSimulationEngine _simulation = new();
    private readonly CritEffectSystem _critEffects = new();
    private readonly DispatcherTimer _timer;

    public static readonly StyledProperty<string> RollResultTextProperty =
        AvaloniaProperty.Register<RpgDiceTrayControl, string>(
            nameof(RollResultText), "Ready to roll", defaultBindingMode: BindingMode.TwoWay);

    public string RollResultText
    {
        get => GetValue(RollResultTextProperty);
        set => SetValue(RollResultTextProperty, value);
    }

    public static readonly StyledProperty<int> LastRollTotalProperty =
        AvaloniaProperty.Register<RpgDiceTrayControl, int>(
            nameof(LastRollTotal), defaultBindingMode: BindingMode.TwoWay);

    public int LastRollTotal
    {
        get => GetValue(LastRollTotalProperty);
        set => SetValue(LastRollTotalProperty, value);
    }

    public static readonly StyledProperty<DiceRollRequest?> RollRequestProperty =
        AvaloniaProperty.Register<RpgDiceTrayControl, DiceRollRequest?>(nameof(RollRequest));

    public DiceRollRequest? RollRequest
    {
        get => GetValue(RollRequestProperty);
        set => SetValue(RollRequestProperty, value);
    }

    public static readonly StyledProperty<ICommand?> RollCompletedCommandProperty =
        AvaloniaProperty.Register<RpgDiceTrayControl, ICommand?>(nameof(RollCompletedCommand));

    public ICommand? RollCompletedCommand
    {
        get => GetValue(RollCompletedCommandProperty);
        set => SetValue(RollCompletedCommandProperty, value);
    }

    static RpgDiceTrayControl()
    {
        RollRequestProperty.Changed.AddClassHandler<RpgDiceTrayControl>((control, e) =>
        {
            if (e.NewValue is DiceRollRequest req)
                control.StartRoll(req);
        });
    }

    public RpgDiceTrayControl()
    {
        _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };
        _timer.Tick += OnTick;
    }

    public void StartRoll(DiceRollRequest request)
    {
        _critEffects.Clear();
        RollResultText = request.IsD100 ? "Rolling d100..." : "Rolling...";

        var diceToThrow = request.IsD100
            ? [DieType.D10Tens, DieType.D10]
            : request.Dice;

        _simulation.ThrowDice(diceToThrow, request.IsD100, (float)Bounds.Width, (float)Bounds.Height);
        _timer.Start();
    }

    private void OnTick(object? sender, EventArgs e)
    {
        const float dt = 0.016f;

        if (_simulation.Step(dt, (float)Bounds.Width, (float)Bounds.Height))
        {
            var outcome = _simulation.EvaluateOutcome();
            LastRollTotal = outcome.Total;
            RollResultText = outcome.Summary;

            if (outcome.CritPosition.HasValue && outcome.IsCritSuccess.HasValue)
                _critEffects.Trigger(outcome.CritPosition.Value, outcome.IsCritSuccess.Value);

            if (RollCompletedCommand?.CanExecute(outcome.Total) == true)
                RollCompletedCommand.Execute(outcome.Total);
        }

        _critEffects.Step(dt);
        InvalidateVisual();

        if (!_simulation.IsRolling && !_critEffects.IsActive)
            _timer.Stop();
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        var center = new Vector2((float)Bounds.Width / 2f, (float)Bounds.Height / 2f);

        _critEffects.RenderBackground(context, center);

        foreach (var die in _simulation.Dice.OrderBy(d => d.Position.Z))
            Die3DRenderer.RenderDie(context, die, center);

        _critEffects.RenderForeground(context, center);
    }
}
