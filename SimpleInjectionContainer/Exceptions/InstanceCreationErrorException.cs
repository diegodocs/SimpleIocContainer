namespace SimpleInjectionContainer.Exceptions
{
    public class InstanceCreationErrorException : Exception
    {
        public InstanceCreationErrorException(string message)
        : base(message)
        {
        }
    }
}