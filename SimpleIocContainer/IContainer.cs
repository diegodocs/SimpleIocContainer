using System;
using System.Collections.Generic;

namespace SimpleIocContainer
{
    public interface IContainer : IDisposable
    {
        void Register<TTypeContract, TTypeImplementation>();
        void Register<TTypeContract, TTypeImplementation>(LifeCycleEnum lifeCycle);
        void RegisterInstance<TTypeContract>(object instance);
        object Resolve(Type typeToResolve);
        TTypeContract Resolve<TTypeContract>();
        IEnumerable<RegisteredObject> RegisteredObjects { get; }
    }
}