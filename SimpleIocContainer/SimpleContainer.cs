namespace SimpleIocContainer
{
    public class SimpleContainer : IContainer
    {
        public IList<RegisteredObject> RegisteredObjects { get; internal set; } = [];

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            this.RegisteredObjects.Clear();
        }

        private List<object> ResolveConstructorParameters(RegisteredObject registeredObject)
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

        public void Register<TContractType, TImplementationType>()
        {
            Register<TContractType, TImplementationType>(LifeCycleEnum.Transient);
        }

        public void Register<TContractType, TImplementationType>(LifeCycleEnum lifeCycle)
        {
            var registeredObject = new RegisteredObject(
                contractType: typeof(TContractType),
                implementationType: typeof(TImplementationType),
                lifeCycle: lifeCycle);

            RegisteredObjects.Add(registeredObject);
        }

        public void RegisterInstance<TContractType>(object instance)
        {
            var registeredObject = new RegisteredObject(
                contractType: typeof(TContractType),
                implementationType: typeof(TContractType),
                instance: instance,
                lifeCycle: LifeCycleEnum.Singleton);

            RegisteredObjects.Add(registeredObject);
        }

        public object Resolve(Type ContractType)
        {
            var registeredObject = RegisteredObjects.FirstOrDefault(o => o.ContractType == ContractType) ??
                throw new TypeNotRegisteredException($"The type {ContractType.Name} has not been registered.");

            if (registeredObject.Instance == null || registeredObject.LifeCycle == LifeCycleEnum.Transient)
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