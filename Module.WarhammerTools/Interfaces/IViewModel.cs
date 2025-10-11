namespace Module.WarhammerTools.Interfaces;

public interface IViewModel<T>
{
    T GetModel();
    
    void SetModel(T model);
}