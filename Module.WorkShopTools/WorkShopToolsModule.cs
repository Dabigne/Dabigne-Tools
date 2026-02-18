using Autofac;
using Module.WorkShopTools.ViewModels;
using Module.WorkShopTools.Views;

namespace Module.WorkShopTools;

public sealed class WorkShopToolsModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ModeExplorerView>().AsSelf();
        builder.RegisterType<ModeExplorerViewModel>().AsSelf();
    }
}