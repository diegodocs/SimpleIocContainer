using SimpleInjectionContainer.Contracts;
using SimpleInjectionContainer.Exceptions;

namespace SimpleInjectionContainer
{
    public class SimpleContainer(IList<TypeRegistered> typesRegistered) : IContainer
    {
        private readonly IList<TypeRegistered> typesRegistered = typesRegistered;

        public int typesRegisteredCount { get { return typesRegistered.Count; } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            typesRegistered.Clear();
        }

        protected List<object> ResolveConstructorParameters(TypeRegistered typeRegistered)
        {
            var resolvedParameters = new List<object>();

            var constructors = typeRegistered.ImplementationType.GetConstructors();

            if (constructors.Length > 0 )
            {
                var constructorInfo = typeRegistered.ImplementationType.GetConstructors()[0];
                var parameters = constructorInfo.GetParameters();

                foreach (var parameter in parameters)
                {
                    resolvedParameters.Add(Resolve(parameter.ParameterType));
                }
            }

            return resolvedParameters;
        }
        private object Resolve(Type ContractType)
        {
            var typeRegistered = typesRegistered.FirstOrDefault(o => o.ContractType == ContractType)
                ?? throw new TypeNotRegisteredException(ContractType.Name);

            if (typeRegistered.Instance == null || typeRegistered.LifeCycle == LifeCycleScope.Transient)
            {
                var parameters = ResolveConstructorParameters(typeRegistered);
                typeRegistered.GetInstance(parameters);
            }

            return typeRegistered.Instance;
        }
        public TContractType Resolve<TContractType>()
        {
            return (TContractType)Resolve(typeof(TContractType));
        }
    }
}