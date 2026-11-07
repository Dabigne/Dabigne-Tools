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
    
    [ObservableProperty]
    private CharacterInformationsViewModel _informations  = new();

    [ObservableProperty] 
    private CharacterCharacteristicListViewModel _characteristicList;
    
    [ObservableProperty]
    private CharacterDestinyViewModel _destiny  = new();
    
    [ObservableProperty]
    private CharacterResilienceViewModel _resilience = new();
    
    [ObservableProperty]
    private CharacterExperienceViewModel _experience = new();
    
    [ObservableProperty]
    private CharacterMovementViewModel _movement = new();

    [ObservableProperty]
    private CharacterExpertiseListViewModel _firstExpertiseList;

    [ObservableProperty] 
    private CharacterExpertiseListViewModel _secondExpertiseList;

    [ObservableProperty]
    private CharacterAdvancedExpertiseListViewModel _advancedExpertiseList;

    [ObservableProperty]
    private CharacterSkillListViewModel _skillList;
    
    [ObservableProperty]
    private CharacterAmbitionsViewModel _ambitions = new();
    
    [ObservableProperty]
    private CharacterGroupViewModel _group = new();

    [ObservableProperty] 
    private CharacterArmorListViewModel _armors;

    [ObservableProperty] 
    private CharacterArmorLocationListViewModel _armorLocations;

    [ObservableProperty] 
    private CharacterPossessionListViewModel _possessions;
    
    [ObservableProperty]
    private CharacterWeaponListViewModel _weapons;

    [ObservableProperty]
    private CharacterPsychologyViewModel _psychology = new();
    
    [ObservableProperty]
    private CharacterCorruptionAndMutationViewModel _corruptionAndMutations = new();
    
    [ObservableProperty]
    private CharacterWealthViewModel _wealth = new();
    
    [ObservableProperty]
    private CharacterClutterViewModel _clutter;
    
    [ObservableProperty]
    private CharacterInjuriesViewModel _injuries;

    [ObservableProperty]
    private CharacterSpellListViewModel _spells;

    [ObservableProperty]
    private CharacterNoteListViewModel _notes = new();

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
        
        Clutter.Refresh();
    }

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrEmpty(LastFilePath))
        {
            var file = await _fileService.PickSaveFile();
            if (file == null) 
                return;
            LastFilePath = file.Path.LocalPath;
        }
        
        _characterSheetFileService.Save(_characterSheetService.UpdateModel(this), LastFilePath);
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

        CharacteristicList = new CharacterCharacteristicListViewModel(_characterSheetService);
        
        FirstExpertiseList = instanceProvider.GetInstance<CharacterExpertiseListViewModel>();
        SecondExpertiseList = instanceProvider.GetInstance<CharacterExpertiseListViewModel>();
        AdvancedExpertiseList = instanceProvider.GetInstance<CharacterAdvancedExpertiseListViewModel>();
        SkillList = instanceProvider.GetInstance<CharacterSkillListViewModel>();
        Armors = instanceProvider.GetInstance<CharacterArmorListViewModel>();
        ArmorLocations = instanceProvider.GetInstance<CharacterArmorLocationListViewModel>();
        Possessions = instanceProvider.GetInstance<CharacterPossessionListViewModel>();
        Weapons = instanceProvider.GetInstance<CharacterWeaponListViewModel>();
        Spells = instanceProvider.GetInstance<CharacterSpellListViewModel>();

        Clutter = instanceProvider.GetInstance<CharacterClutterViewModel>();
        Injuries = instanceProvider.GetInstance<CharacterInjuriesViewModel>();

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