using System.Collections.ObjectModel;
using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Application.Core.ViewModels;

public partial class OutputViewModel : ObservableObject
{
    private readonly IOutputService _outputService;

    public ObservableCollection<string> Content { get; } = [];

    [ObservableProperty] 
    private string? _selectedLine = null;
    
    partial void OnSelectedLineChanged(string? selectedLine)
    {
        
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

    private void Append(string text)
    {
        Content.Add(text);
    }
}