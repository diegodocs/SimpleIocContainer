﻿namespace SimpleInjectionContainer.Contracts
{
    public interface IContainer : IDisposable
    {
        TContractType Resolve<TContractType>();
        int RegisteredObjectsCount { get; }
    }
}