using SimpleInjectionContainer.Contracts;
using SimpleInjectionContainer.Exceptions;

namespace SimpleInjectionContainer
{
    public class SimpleContainer(IList<RegisteredType> typesRegistered) : IContainer
    {
        private readonly IList<RegisteredType> registeredTypes = typesRegistered;

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

        protected List<object> ResolveDependencies(RegisteredType typeRegistered)
        {
            var resolvedParameters = new List<object>();

            var constructorInfo = typeRegistered.ImplementationType.GetConstructors().FirstOrDefault();

            if (constructorInfo != null)
            {               
                var parameters = constructorInfo.GetParameters();

                foreach (var parameter in parameters)
                {
                    resolvedParameters.Add(Resolve(parameter.ParameterType));
                }
            }

            return resolvedParameters;
        }
        protected object Resolve(Type ContractType)
        {
            var typeRegistered = registeredTypes.FirstOrDefault(o => o.ContractType == ContractType) ?? 
                throw new TypeNotRegisteredException(ContractType.Name);

            if (typeRegistered.Instance == null || typeRegistered.LifeCycle == LifeCycleScope.Transient)
            {                
                typeRegistered.CreateInstance(
                    ResolveDependencies(typeRegistered));
            }

            if (typeRegistered.Instance == null)
                throw new NullInstanceException(ContractType.Name);

            return typeRegistered.Instance;
        }
        public TContractType Resolve<TContractType>()
        {
            return (TContractType)Resolve(typeof(TContractType));
        }
    }
}