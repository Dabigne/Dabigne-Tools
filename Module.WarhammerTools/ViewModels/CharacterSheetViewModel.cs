using Application.Core.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Models;
using Module.WarhammerTools.ViewModels.Components;

namespace Module.WarhammerTools.ViewModels;

public sealed partial class CharacterSheetViewModel : ObservableObject
{
    private readonly IFileService _fileService;
    private readonly ICharacterSheetFileService  _characterSheetFileService;
    
    public CharacterInformationsViewModel Informations;
    
    public CharacterCharacteristicListViewModel CharacteristicList { get; }  = new();
    
    public CharacterDestinyViewModel Destiny { get; }  = new();
    
    public CharacterResilienceViewModel Resilience { get; }  = new();
    
    public CharacterExperienceViewModel Experience { get; }  = new();
    
    public CharacterMovementViewModel Movement { get; }  = new();
    
    public CharacterExpertiseListViewModel FirstExpertiseList { get; }  = new();

    public CharacterExpertiseListViewModel SecondExpertiseList { get; }  = new();

    [RelayCommand]
    private async Task Load()
    {
        var file = await _fileService.PickLoadFile();
        if (file == null) 
            return;

        var model = _characterSheetFileService.Load(file.Path.LocalPath);
        SetModel(model);
    }

    [RelayCommand]
    private async Task Save()
    {
        var file = await _fileService.PickSaveFile();
        if (file == null) 
            return;
        
        _characterSheetFileService.Save(GetModel(), file.Path.LocalPath);
    }
    
    public CharacterSheetViewModel(
        ICharacterSheetService characterSheetService,
        IFileService  fileService,
        ICharacterSheetFileService characterSheetFileService)
    {
        _fileService = fileService;
        _characterSheetFileService = characterSheetFileService;
        Informations = new CharacterInformationsViewModel();
        
        SetModel(characterSheetService.BuildCharacterSheet());
    }

    private void SetModel(CharacterSheet characterSheet)
    {
        Informations.SetModel(characterSheet.Informations);
        CharacteristicList.SetModel(characterSheet.Characteristics);
        Destiny.SetModel(characterSheet.Destiny);
        Resilience.SetModel(characterSheet.Resilience);
        Experience.SetModel(characterSheet.Experience);
        Movement.SetModel(characterSheet.Movement);
        SetExpertises(characterSheet.Expertises, GetModel().Characteristics);
    }

    private void SetExpertises(
        IList<CharacterExpertise> expertises, 
        IList<CharacterCharacteristic> characteristics)
    {
        var halfCount = expertises.Count / 2;
        var firstList = expertises.Take(halfCount).ToList();
        var secondList = expertises.Skip(halfCount).ToList();
        FirstExpertiseList.SetModel(firstList, characteristics);
        SecondExpertiseList.SetModel(secondList, characteristics);
    }

    private CharacterSheet GetModel()
    {
        var model = new CharacterSheet();
        model.Informations = Informations.GetModel();
        model.Characteristics = CharacteristicList.GetModel();
        model.Destiny = Destiny.GetModel();
        model.Resilience = Resilience.GetModel();
        model.Experience = Experience.GetModel();
        model.Movement = Movement.GetModel();
        model.Expertises = GetExpertises();
        
        return model;
    }

    private IList<CharacterExpertise> GetExpertises()
    {
        var list = FirstExpertiseList.GetModel();
        foreach (var expertise in SecondExpertiseList.GetModel())
        {
            list.Add(expertise);
        }
        return list;
    }
}