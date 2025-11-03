using Application.Core.Interfaces.Services;
using Autofac;
using Autofac.Core;

namespace Application.Core.Services;

public class InstanceProvider : IInstanceProvider
{
    private IContainer? _container;
    
    public void Start(IList<Module> modules)
    {
        var containerBuilder = new ContainerBuilder();
        foreach (var module in modules)
            containerBuilder.RegisterModule(module);
        containerBuilder.RegisterInstance(this).As<IInstanceProvider>();
        _container = containerBuilder.Build();
    }
    
    public T GetInstance<T>() where T : notnull
    {
        return _container!.Resolve<T>();
    }

    public object GetInstance(Type type)
    {
        return _container!.Resolve(type);
    }

    public IList<Type> GetRegisteredTypes<T>()
    {
        var registrations = _container!.ComponentRegistry.Registrations;
        var result = new List<Type>();
        
        foreach (var registration in registrations)
        {
            foreach (var service in registration.Services.Where(s => s is TypedService))
            {
                var typedService = (TypedService)service;
                if(typedService.ServiceType.IsAssignableTo<T>())
                    result.Add(typedService.ServiceType);
            }
        }

        return result;
    }
}