using Autofac;
using Module.WarhammerTools.Interfaces;
using Module.WarhammerTools.Services;
using Module.WarhammerTools.ViewModels;
using Module.WarhammerTools.Views;

namespace Module.WarhammerTools;

public sealed class WarhammerToolsModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        LoadServices(builder);
        
        builder.RegisterType<CharacterSheetView>().AsSelf();
        builder.RegisterType<CharacterSheetViewModel>().AsSelf();
    }

    private void LoadServices(ContainerBuilder builder)
    {
        builder.RegisterType<CharacterSheetService>().As<ICharacterSheetService>();
        builder.RegisterType<CharacterSheetFileService>().As<ICharacterSheetFileService>();
        builder.RegisterType<TemplateService>().AsSelf();
    }
}