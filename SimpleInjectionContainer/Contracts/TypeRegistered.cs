using SimpleInjectionContainer.Exceptions;

namespace SimpleInjectionContainer.Contracts
{

    public class TypeRegistered(Type contractType, Type implementationType, object? instance = null, LifeCycleScope lifeCycle = LifeCycleScope.Transient) : ITypeRegistered
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public Type ContractType { get; protected set; } = contractType;
        public Type ImplementationType { get; protected set; } = implementationType;
        public object? Instance { get; protected set; } = instance;
        public LifeCycleScope LifeCycle { get; protected set; } = lifeCycle;
        public void GetInstance(IEnumerable<object> parameters)
        {
            var new_instance = Activator.CreateInstance(type: ImplementationType, [.. parameters])
                ?? throw new NullInstanceException(ContractType.Name);

            Instance = new_instance;
        }
    }
}