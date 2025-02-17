namespace SimpleIocContainer
{
    public interface IContainer : IDisposable
    {
        void Register<TContractType, TImplementationType>(LifeCycleEnum lifeCycle);
        void RegisterInstance<TContractType>(object instance);
        TContractType Resolve<TContractType>();
        IList<RegisteredObject> RegisteredObjects { get; }
    }
}