namespace SimpleInjectionContainerTests.Mock
{
    public class MockApplicationService : IApplicationService
    {
        protected IFileHandler fileHandler;
        public MockApplicationService(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public void SaveUserLogToFile(string login)
        {
            fileHandler.Save("UserAuth", @"c:\log\userAuth.txt");
            fileHandler.Save("UserOrders", @"c:\log\userOrders.txt");
            fileHandler.Save("UserPayments", @"c:\log\userPayments.txt");
        }
    }
}