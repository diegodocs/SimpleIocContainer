namespace SimpleIoCContainer.Contracts
{
    public interface IContainer : IDisposable
    {
        void Register<TContractType, TImplementationType>(ObjectLifeCycle lifeCycle);
        void RegisterInstance<TContractType>(object instance);
        TContractType Resolve<TContractType>();
        IList<RegisteredObject> RegisteredObjects { get; }
    }
}