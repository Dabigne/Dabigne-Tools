using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Application.Core.ViewModels;

public partial class OutputViewModel : ObservableObject
{
    public ObservableCollection<string> Content { get; } = ["Test 1", "Test 2"];

    [RelayCommand]
    private void Clear()
    {
        Content.Clear();
    }

    public void Append(string text)
    {
        Content.Add(text);
    }
}