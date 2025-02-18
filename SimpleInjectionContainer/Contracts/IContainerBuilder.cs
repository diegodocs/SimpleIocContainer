namespace SimpleInjectionContainer.Contracts
{
    public interface IContainerBuilder : IDisposable
    {
        int typesRegisteredCount { get; }
        void Register<TImplementationType>();
        void Register<TContractType, TImplementationType>();
        void Register<TContractType, TImplementationType>(LifeCycleScope lifeCycle);
        void RegisterInstance<TContractType>(object instance);
        IContainer Build();
    }
}