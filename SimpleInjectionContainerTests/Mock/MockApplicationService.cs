namespace SimpleInjectionContainerTests.Mock
{
    public class MockApplicationService(IFileHandler fileHandler) : IApplicationService
    {
        protected IFileHandler fileHandler = fileHandler;

        public void SaveUserLogToFile(string login)
        {
            fileHandler.Save("UserAuth", @"c:\log\userAuth.txt");
            fileHandler.Save("UserOrders", @"c:\log\userOrders.txt");
            fileHandler.Save("UserPayments", @"c:\log\userPayments.txt");
        }
    }
}