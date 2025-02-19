using SimpleInjectionContainer.Contracts;

namespace SimpleInjectionContainer
{
    public class ContainerBuilder : IContainerBuilder
    {
        protected readonly IList<RegisteredType> registeredTypes = [];
        public int TypesRegisteredCount => registeredTypes.Count;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                registeredTypes.Clear();
            }
        }

        public void Register<TImplementationType>()
        {
            Register<TImplementationType, TImplementationType>(LifeCycleScope.Transient);
        }

        public void Register<TContractType, TImplementationType>()
        {
            Register<TContractType, TImplementationType>(LifeCycleScope.Transient);
        }

        public void Register<TContractType, TImplementationType>(LifeCycleScope lifeCycle)
        {
            var typeRegistered = new RegisteredType(
                contractType: typeof(TContractType),
                implementationType: typeof(TImplementationType),
                lifeCycle: lifeCycle);

            registeredTypes.Add(typeRegistered);
        }

        public void Register<TContractType>(object instance)
        {
            var typeRegistered = new RegisteredType(
                contractType: typeof(TContractType),                
                instance: instance);

            registeredTypes.Add(typeRegistered);
        }

        public void Register(object instance)
        {
            var typeRegistered = new RegisteredType(                
                instance: instance);

            registeredTypes.Add(typeRegistered);
        }

        public IContainer Build()
        {
            return new SimpleContainer(registeredTypes);
        }
    }
}