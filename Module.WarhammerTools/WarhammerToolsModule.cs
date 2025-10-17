using Autofac;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Services;
using Module.WarhammerTools.ViewModels;
using Module.WarhammerTools.ViewModels.Components;
using Module.WarhammerTools.Views;

namespace Module.WarhammerTools;

public sealed class WarhammerToolsModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        LoadServices(builder);
        LoadComponents(builder);
        LoadViewModels(builder);
    }

    private void LoadViewModels(ContainerBuilder builder)
    {
        builder.RegisterType<CharacterSheetView>().AsSelf();
        builder.RegisterType<CharacterSheetViewModel>().AsSelf();
    }
    
    private void LoadServices(ContainerBuilder builder)
    {
        builder.RegisterType<CharacterSheetService>().As<ICharacterSheetService>().SingleInstance();
        builder.RegisterType<CharacterSheetFileService>().As<ICharacterSheetFileService>();
        builder.RegisterType<CharacteristicRulesService>().As<ICharacteristicRulesService>();
        builder.RegisterType<TemplateService>().AsSelf();
    }

    private void LoadComponents(ContainerBuilder builder)
    {
        builder.RegisterType<CharacterWeaponViewModel>().AsSelf().InstancePerDependency();
        builder.RegisterType<CharacterArmorViewModel>().AsSelf().InstancePerDependency();
        builder.RegisterType<CharacterSkillViewModel>().AsSelf().InstancePerDependency();
        builder.RegisterType<CharacterExpertiseViewModel>().AsSelf().InstancePerDependency();
        builder.RegisterType<CharacterPossessionViewModel>().AsSelf().InstancePerDependency();
        builder.RegisterType<CharacterSpellViewModel>().AsSelf().InstancePerDependency();
    }
}