using SimpleInjectionContainer.Exceptions;

namespace SimpleInjectionContainer.Contracts
{

    public class TypeRegistered : ITypeRegistered
    {
        public Guid Id { get; protected set; } 
        public Type ContractType { get; protected set; } 
        public Type ImplementationType { get; protected set; } 
        public object Instance { get; protected set; } 
        public LifeCycleScope LifeCycle { get; protected set; } 

        public TypeRegistered(Type contractType, Type implementationType, LifeCycleScope lifeCycle = LifeCycleScope.Transient)
        {
            Id = Guid.NewGuid();
            ContractType = contractType;    
            ImplementationType = implementationType;            
            LifeCycle = lifeCycle;
        }

        public TypeRegistered(Type contractType, Type implementationType, object instance, LifeCycleScope lifeCycle = LifeCycleScope.Transient)
           : this(contractType, implementationType, lifeCycle)
        {
            Instance = instance;
        }

        public TypeRegistered(Type contractType, object instance)
            :this(contractType, instance.GetType(), instance, LifeCycleScope.Singleton)
        {            
        }
        public TypeRegistered(object instance)
            : this(instance.GetType(), instance.GetType(), instance, LifeCycleScope.Singleton)
        {
        }
        public void GetInstance(IEnumerable<object> parameters)
        {
            var new_instance = Activator.CreateInstance(type: ImplementationType, [.. parameters])
                ?? throw new NullInstanceException(ContractType.Name);

            Instance = new_instance;
        }
    }
}