using SimpleInjectionContainer.Exceptions;

namespace SimpleInjectionContainer.Contracts
{

    public class RegisteredType(Type contractType, Type implementationType, object? instance = null, LifeCycleScope lifeCycle = LifeCycleScope.Transient) : IRegisteredType
    {
        public object? Instance { get; protected set; } = instance;
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public Type ContractType { get; protected set; } = contractType;
        public Type ImplementationType { get; protected set; } = implementationType;
        public LifeCycleScope LifeCycle { get; protected set; } = lifeCycle;

        public RegisteredType(Type contractType, object instance)
            : this(contractType, instance.GetType(), instance, LifeCycleScope.Singleton)
        {
            Instance = instance;
        }
        public RegisteredType(object instance)
            : this(instance.GetType(), instance.GetType(), instance, LifeCycleScope.Singleton)
        {
        }
        public void CreateInstance(IEnumerable<object> parameters)
        {
            Instance = Activator.CreateInstance(type: ImplementationType, [.. parameters]) ??
                    throw new NullInstanceException(ContractType.Name);
            
        }
    }
}