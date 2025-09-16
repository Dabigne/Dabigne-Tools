using System.Collections.ObjectModel;
using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Application.Core.ViewModels;

public class OutputViewModelDesign : OutputViewModel
{
    public OutputViewModelDesign() : base(null) {}
}

public partial class OutputViewModel : ObservableObject
{
    private readonly IOutputService _outputService;

    public ObservableCollection<IOutputLine> Content { get; } = [];

    [ObservableProperty] 
    private IOutputLine? _selectedLine = null;
    
    partial void OnSelectedLineChanged(IOutputLine? selectedLine)
    {
        if (SelectedLine != null) 
            _outputService.SelectLine(SelectedLine);
    }

    [RelayCommand]
    private void Clear()
    {
        Content.Clear();
        SelectedLine = null;
    }

    public OutputViewModel(IOutputService outputService)
    {
        _outputService = outputService;
        _outputService.LinePushed += Append;
    }

    private void Append(IOutputLine line)
    {
        Content.Add(line);
    }
}