using Autofac;
using Module.WarhammerTools.Views;

namespace Module.WarhammerTools;

public class WarhammerToolsModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CharacterSheetView>().AsSelf();
    }
}