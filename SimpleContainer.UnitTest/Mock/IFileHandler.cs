namespace SimpleIocContainer.UnitTest.Mock
{

    public interface IFileHandler
    {
        void Save(string content, string path);
    }
}