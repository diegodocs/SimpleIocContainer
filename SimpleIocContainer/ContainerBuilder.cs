
using SimpleInjectionContainer.Contracts;

namespace SimpleInjectionContainer
{
    public class ContainerBuilder : IContainerBuilder
    {
        protected readonly IList<RegisteredObject> registeredObjects = [];

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {            
            registeredObjects.Clear();
        }

        public void Register<TContractType, TImplementationType>()
        {
            Register<TContractType, TImplementationType>(LifeCycleScope.Transient);
        }

        public void Register<TContractType, TImplementationType>(LifeCycleScope lifeCycle)
        {
            var registeredObject = new RegisteredObject(
                contractType: typeof(TContractType),
                implementationType: typeof(TImplementationType),
                lifeCycle: lifeCycle);

            registeredObjects.Add(registeredObject);
        }

        public void RegisterInstance<TContractType>(object instance)
        {
            var registeredObject = new RegisteredObject(
                contractType: typeof(TContractType),
                implementationType: typeof(TContractType),
                instance: instance,
                lifeCycle: LifeCycleScope.Singleton);

            registeredObjects.Add(registeredObject);
        }

        public IContainer Build()
        {
            return new SimpleContainer(registeredObjects);
        }
    }
}