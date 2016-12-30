using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleIocContainer
{

    public class SimpleContainerIoc : IContainer
    {
        private IList<RegisteredObject> registeredObjects = new List<RegisteredObject>();
        public IEnumerable<RegisteredObject> RegisteredObjects { get { return registeredObjects; } }

        public void Dispose()
        {
            registeredObjects = null;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var resolvedParameters = new List<object>();
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            var parameters = constructorInfo.GetParameters();

            foreach (var parameter in parameters)
            {
                resolvedParameters.Add(ResolveObject(parameter.ParameterType));
            }

            return resolvedParameters;
        }

        private object ResolveObject(Type typeToResolve)
        {
            var registeredObject = registeredObjects.FirstOrDefault(o => o.TypeToResolve == typeToResolve);

            if (registeredObject == null)
            {
                throw new Exception($"The type {typeToResolve.Name} has not been registered.");
            }

            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.Instance == null || registeredObject.LifeCycle == LifeCycleEnum.Transient)
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                registeredObject.GetInstance(parameters);
            }

            return registeredObject.Instance;
        }

        public void Register<TTypeContract, TTypeImplementation>()
        {
            Register<TTypeContract, TTypeImplementation>(LifeCycleEnum.Transient);
        }

        public void Register<TTypeContract, TTypeImplementation>(LifeCycleEnum lifeCycle)
        {
            var registeredObject = new RegisteredObject
            {
                TypeToResolve = typeof(TTypeContract),
                ConcreteType = typeof(TTypeImplementation),
                LifeCycle = lifeCycle
            };

            registeredObjects.Add(registeredObject);
        }

        public void RegisterInstance<TTypeContract>(object instance)
        {
            var registeredObject = new RegisteredObject
            {
                TypeToResolve = typeof(TTypeContract),
                ConcreteType = typeof(TTypeContract),
                Instance = instance,
                LifeCycle = LifeCycleEnum.Singleton
            };

            registeredObjects.Add(registeredObject);
        }

        public object Resolve(Type typeToResolve)
        {
            return ResolveObject(typeToResolve);
        }

        public TTypeContract Resolve<TTypeContract>()
        {
            return (TTypeContract)ResolveObject(typeof(TTypeContract));
        }
    }
}