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
    private readonly ICharacterSheetFileService  _characterSheetFileService;

    private readonly IFileService _fileService;
    private readonly ISessionService _sessionService;
    
    public string? LastFilePath { get; private set; }
    
    public CharacterInformationsViewModel Informations { get; } = new();
    
    public CharacterCharacteristicListViewModel CharacteristicList { get; }  = new();
    
    public CharacterDestinyViewModel Destiny { get; }  = new();
    
    public CharacterResilienceViewModel Resilience { get; }  = new();
    
    public CharacterExperienceViewModel Experience { get; }  = new();
    
    public CharacterMovementViewModel Movement { get; }  = new();
    
    public CharacterExpertiseListViewModel FirstExpertiseList { get; }

    public CharacterExpertiseListViewModel SecondExpertiseList { get; }
    
    public CharacterAdvancedExpertiseListViewModel AdvancedExpertiseList { get; }

    public CharacterSkillListViewModel SkillList { get; }
    
    public CharacterAmbitionsViewModel Ambitions { get; } = new();
    
    public CharacterGroupViewModel Group { get; } = new();

    public CharacterArmorListViewModel Armors { get; }
    
    public CharacterArmorLocationListViewModel ArmorLocations { get; }

    public CharacterPossessionListViewModel Possessions { get; }
    
    public CharacterWeaponListViewModel  Weapons { get; }

    public CharacterPsychologyViewModel Psychology { get; } = new();
    
    public CharacterCorruptionAndMutationViewModel CorruptionAndMutations { get; } = new();
    
    public CharacterWealthViewModel Wealth { get; } = new();
    
    public CharacterClutterViewModel Clutter { get; }
    
    public CharacterInjuriesViewModel Injuries { get; }

    public CharacterSpellListViewModel Spells { get; }
    
    public CharacterNoteListViewModel Notes { get; } = new();
    
    [RelayCommand]
    private async Task Load()
    {
        var file = await _fileService.PickLoadFile();
        if (file == null) 
            return;

        _sessionService.Parameters = file.Path.LocalPath; 
        LoadCharacterSheet(file.Path.LocalPath);
    }

    public void LoadCharacterSheet(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return;
        
        LastFilePath = filePath;

        _characterSheetService.LoadModel(_characterSheetFileService.Load(filePath));
        _characterSheetService.InjectModel(this);
    }

    [RelayCommand]
    private async Task Save()
    {
        var file = await _fileService.PickSaveFile();
        if (file == null) 
            return;
        
        LastFilePath = file.Path.LocalPath;
        _characterSheetFileService.Save(_characterSheetService.UpdateModel(this), file.Path.LocalPath);
    }
    
    public CharacterSheetViewModel(
        ICharacterSheetService characterSheetService,
        ICharacterSheetFileService characterSheetFileService,
        IFileService  fileService,
        ISessionService sessionService,
        IInstanceProvider instanceProvider)
    {
        _characterSheetService = characterSheetService;
        _characterSheetFileService = characterSheetFileService;
        _fileService = fileService;
        _sessionService = sessionService;
        
        FirstExpertiseList = new CharacterExpertiseListViewModel(instanceProvider);
        SecondExpertiseList = new CharacterExpertiseListViewModel(instanceProvider);
        AdvancedExpertiseList = new CharacterAdvancedExpertiseListViewModel(instanceProvider);
        SkillList = new CharacterSkillListViewModel(instanceProvider);
        Armors = new CharacterArmorListViewModel(instanceProvider);
        ArmorLocations = new CharacterArmorLocationListViewModel(instanceProvider.GetInstance<IArmorRulesService>());
        Possessions = new CharacterPossessionListViewModel(instanceProvider);
        Weapons = new CharacterWeaponListViewModel(instanceProvider);
        Spells = new CharacterSpellListViewModel(instanceProvider);

        Clutter = new CharacterClutterViewModel(instanceProvider.GetInstance<IClutterRulesService>());
        Injuries = new CharacterInjuriesViewModel(instanceProvider.GetInstance<ICharacteristicRulesService>());
        
        _characterSheetService.InjectModel(this);
    }
    
    public void SetExpertises(IList<CharacterExpertise> expertises)
    {
        var halfCount = expertises.Count / 2;
        var firstList = expertises.Take(halfCount).ToList();
        var secondList = expertises.Skip(halfCount).ToList();
        FirstExpertiseList.SetModel(firstList);
        SecondExpertiseList.SetModel(secondList);
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