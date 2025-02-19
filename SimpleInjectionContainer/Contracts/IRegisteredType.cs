
namespace SimpleInjectionContainer.Contracts
{
    public interface IRegisteredType
    {
        Guid Id { get; }
        object? Instance { get; }
        Type ContractType { get; }
        Type ImplementationType { get; }
        LifeCycleScope LifeCycle { get; }
        void CreateInstance(IEnumerable<object> parameters);
    }
}