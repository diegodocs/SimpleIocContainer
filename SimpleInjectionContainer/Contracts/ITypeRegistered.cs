
namespace SimpleInjectionContainer.Contracts
{
    public interface ITypeRegistered
    {
        Guid Id { get; }
        Type ContractType { get; }        
        Type ImplementationType { get; }
        object? Instance { get; }
        LifeCycleScope LifeCycle { get; }
        void GetInstance(IEnumerable<object> parameters);
    }
}