using Autofac;
using Module.PdfTools.Views;

namespace Module.PdfTools;

public class PdfToolsModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PdfFromImagesView>().AsSelf();
    }
}