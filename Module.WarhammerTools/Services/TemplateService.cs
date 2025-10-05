using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;
using Module.WarhammerTools.ViewModels.Components;
using Module.WarhammerTools.Views.Components;

namespace Module.WarhammerTools.Services;

public sealed class TemplateService
{
    public List<IDataTemplate> GetTemplates()
    {
        var templates = new List<IDataTemplate>
        {
            GetTemplate<CharacterCharacteristicListViewModel, CharacterCharacteristicListView>(),
            GetTemplate<CharacterDestinyViewModel, CharacterDestinyView>(),
            GetTemplate<CharacterExperienceViewModel, CharacterExperienceView>(),
            GetTemplate<CharacterExpertiseViewModel, CharacterExpertiseView>(),
            GetTemplate<CharacterInformationsViewModel, CharacterInformationsView>(),
            GetTemplate<CharacterMovementViewModel, CharacterMovementView>(),
            GetTemplate<CharacterResilienceViewModel, CharacterResilienceView>(),
        };

        return templates;
    }
    
    private IDataTemplate GetTemplate<TVm, TV>() where TVm : ObservableObject where TV : Control, new()
    {
        return new FuncDataTemplate<TVm>((value, namescope) => new TV());
    }
}