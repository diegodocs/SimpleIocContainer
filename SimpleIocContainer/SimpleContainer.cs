using SimpleIoCContainer.Contracts;
using SimpleIoCContainer.Exceptions;

namespace SimpleIocContainer
{
    public class SimpleContainer(IList<RegisteredObject> registeredObjects) : IContainer
    {
        private readonly IList<RegisteredObject> registeredObjects = registeredObjects;

        public int RegisteredObjectsCount { get { return registeredObjects.Count; } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            this.registeredObjects.Clear();
        }

        protected List<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var resolvedParameters = new List<object>();
            var constructorInfo = registeredObject.ImplementationType.GetConstructors()[0];
            var parameters = constructorInfo.GetParameters();

            foreach (var parameter in parameters)
            {
                resolvedParameters.Add(Resolve(parameter.ParameterType));
            }

            return resolvedParameters;
        }
        private object Resolve(Type ContractType)
        {
            var registeredObject = registeredObjects.FirstOrDefault(o => o.ContractType == ContractType) ??
                throw new TypeNotRegisteredException($"The type {ContractType.Name} has not been registered.");

            if (registeredObject.Instance == null || registeredObject.LifeCycle == LifeCycleScope.Transient)
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                registeredObject.GetInstance(parameters);
            }

            if (registeredObject.Instance == null)
            {
                var message = $"The type {ContractType.Name} failed to create instance.";
                throw new InstanceCreationErrorException(message);
            }

            return registeredObject.Instance;
        }
        public TContractType Resolve<TContractType>()
        {
            return (TContractType)Resolve(typeof(TContractType));
        }
    }
}