namespace SimpleIoCContainer.Contracts
{
    public interface IContainerBuilder : IDisposable
    {
        void Register<TContractType, TImplementationType>();
        void Register<TContractType, TImplementationType>(LifeCycleScope lifeCycle);
        void RegisterInstance<TContractType>(object instance);
        IContainer Build();
    }
}