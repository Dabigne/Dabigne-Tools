using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.ViewModels;

public sealed partial class CharacterSheetViewModel : ObservableObject
{
    private readonly ICharacterSheetService _characterSheetService;
    private readonly IFileService _fileService;
    private readonly ICharacterSheetFileService  _characterSheetFileService;

    public CharacterInformationsViewModel Informations { get; } = new();
    
    public CharacterCharacteristicListViewModel CharacteristicList { get; }  = new();
    
    public CharacterDestinyViewModel Destiny { get; }  = new();
    
    public CharacterResilienceViewModel Resilience { get; }  = new();
    
    public CharacterExperienceViewModel Experience { get; }  = new();
    
    public CharacterMovementViewModel Movement { get; }  = new();
    
    public CharacterExpertiseListViewModel FirstExpertiseList { get; }  = new();

    public CharacterExpertiseListViewModel SecondExpertiseList { get; }  = new();
    
    public CharacterAdvancedExpertiseListViewModel AdvancedExpertiseList { get; }  = new();

    public CharacterSkillListViewModel SkillList { get; }  = new();
    
    public CharacterAmbitionsViewModel Ambitions { get; } = new();
    
    public CharacterGroupViewModel Group { get; } = new();
    
    public CharacterArmorListViewModel Armors { get; } = new();
    
    public CharacterPossessionListViewModel Possessions { get; } = new();
    
    public CharacterWeaponListViewModel  Weapons { get; } = new();
    
    [RelayCommand]
    private async Task Load()
    {
        var file = await _fileService.PickLoadFile();
        if (file == null) 
            return;

        var model = _characterSheetFileService.Load(file.Path.LocalPath);
        _characterSheetService.SetModel(this, model);
    }

    [RelayCommand]
    private async Task Save()
    {
        var file = await _fileService.PickSaveFile();
        if (file == null) 
            return;
        
        _characterSheetFileService.Save(_characterSheetService.GetModel(this), file.Path.LocalPath);
    }
    
    public CharacterSheetViewModel(
        ICharacterSheetService characterSheetService,
        IFileService  fileService,
        ICharacterSheetFileService characterSheetFileService)
    {
        _characterSheetService = characterSheetService;
        _fileService = fileService;
        _characterSheetFileService = characterSheetFileService;
        
        _characterSheetService.SetModel(this, characterSheetService.BuildCharacterSheet());
    }
    
    public void SetExpertises(
        IList<CharacterExpertise> expertises, 
        IList<CharacterCharacteristic> characteristics)
    {
        var halfCount = expertises.Count / 2;
        var firstList = expertises.Take(halfCount).ToList();
        var secondList = expertises.Skip(halfCount).ToList();
        FirstExpertiseList.SetModel(firstList, characteristics);
        SecondExpertiseList.SetModel(secondList, characteristics);
    }
    
    public IList<CharacterExpertise> GetExpertises()
    {
        var list = FirstExpertiseList.GetModel();
        foreach (var expertise in SecondExpertiseList.GetModel())
        {
            list.Add(expertise);
        }
        return list;
    }
}