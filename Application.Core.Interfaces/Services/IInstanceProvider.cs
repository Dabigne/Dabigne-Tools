namespace Application.Core.Interfaces.Services;

public interface IInstanceProvider
{
    void Start(IList<Autofac.Module> modules);

    T GetInstance<T>() where T : notnull;
    
    object GetInstance(Type type);

    IList<Type> GetRegisteredTypes<T>();
}