﻿namespace SimpleIocContainer
{
    public class TypeNotRegisteredException : Exception 
    {
        public TypeNotRegisteredException(string message)
        : base(message)
        {
        }
    }
}