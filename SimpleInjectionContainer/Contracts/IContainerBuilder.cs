namespace SimpleInjectionContainer.Contracts
{
    public interface IContainerBuilder : IDisposable
    {
        int TypesRegisteredCount { get; }
        void Register<TImplementationType>();
        void Register<TContractType, TImplementationType>();
        void Register<TContractType, TImplementationType>(LifeCycleScope lifeCycle);
        void Register<TContractType>(object instance);
        void Register(object instance);
        IContainer Build();
    }
}