namespace SimpleInjectionContainerTests.Mock
{
    public class MockFileHandler : IFileHandler
    {
        public void Save(string content, string path)
        {
            Console.WriteLine("File Saved Successfully.");
        }
    }
}