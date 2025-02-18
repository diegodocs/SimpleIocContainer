namespace SimpleInjectionContainer.Exceptions
{
    public class NullInstanceException(string typeName) : 
        Exception($"The type {typeName} failed to create instance.")
    {
    }
}