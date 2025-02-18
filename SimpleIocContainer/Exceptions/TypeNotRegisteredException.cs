namespace SimpleInjectionContainer.Exceptions
{
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string message)
        : base(message)
        {
        }
    }
}