using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleIocContainer
{

    public class RegisteredObject
    {
        public Type ConcreteType { get; internal set; }
        public object Instance { get; internal set; }
        public LifeCycleEnum LifeCycle { get; internal set; }
        public Type TypeToResolve { get; internal set; }
        public void GetInstance(IEnumerable<object> parameters)
        {
            Instance = Activator.CreateInstance(ConcreteType, parameters.ToArray());
        }
    }
}