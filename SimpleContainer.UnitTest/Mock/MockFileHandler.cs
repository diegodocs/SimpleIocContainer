using System;

namespace SimpleIocContainer.UnitTest.Mock
{

    public class MockFileHandler : IFileHandler
    {
        public void Save(string content, string path)
        {
            Console.WriteLine("File Saved Successfully.");
        }
    }
}