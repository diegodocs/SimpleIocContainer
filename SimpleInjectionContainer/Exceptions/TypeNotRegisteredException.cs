namespace SimpleInjectionContainer.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string typeName)        
            : base($"The type {typeName} has not been registered.")
        {                        
        }
    }
}