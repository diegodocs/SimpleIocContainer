namespace SimpleIocContainer
{

    public class RegisteredObject(Type contractType, Type implementationType, object? instance = null, LifeCycleEnum lifeCycle = LifeCycleEnum.Transient)
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public Type ImplementationType { get; protected set; } = implementationType;
        public object? Instance { get; protected set; } = instance;
        public LifeCycleEnum LifeCycle { get; protected set; } = lifeCycle;
        public Type ContractType { get; protected set; } = contractType;
        public void GetInstance(IEnumerable<object> parameters)
        {
            Instance = Activator.CreateInstance(type: ImplementationType, [.. parameters]);
        }
    }
}