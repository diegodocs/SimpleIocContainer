using SimpleInjectionContainer.Contracts;

namespace SimpleInjectionContainer
{
    public class ContainerBuilder : IContainerBuilder
    {
        protected readonly IList<TypeRegistered> typesRegistered = [];
        public int TypesRegisteredCount { get { return typesRegistered.Count; } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            typesRegistered.Clear();
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
            var typeRegistered = new TypeRegistered(
                contractType: typeof(TContractType),
                implementationType: typeof(TImplementationType),
                lifeCycle: lifeCycle);

            typesRegistered.Add(typeRegistered);
        }

        public void Register<TContractType>(object instance)
        {
            var typeRegistered = new TypeRegistered(
                contractType: typeof(TContractType),                
                instance: instance);

            typesRegistered.Add(typeRegistered);
        }

        public void Register(object instance)
        {
            var typeRegistered = new TypeRegistered(                
                instance: instance);

            typesRegistered.Add(typeRegistered);
        }

        public IContainer Build()
        {
            return new SimpleContainer(typesRegistered);
        }
    }
}