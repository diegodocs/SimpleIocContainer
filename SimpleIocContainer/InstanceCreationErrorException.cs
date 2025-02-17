namespace SimpleIocContainer
{
    public class InstanceCreationErrorException : Exception
    {
        public InstanceCreationErrorException(string message)
        : base(message)
        {
        }
    }
}