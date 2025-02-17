using SimpleIocContainer.UnitTest.Mock;
using Xunit;

namespace SimpleIoCContainer.Tests.Test
{
    public class RegisteredObjectTests
    {
        [Fact]
        public void WhenGetInstanceIsCalled_Then_InstanceIsCreated()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var container = new SimpleIocContainer.SimpleContainer();
            container.Register<IFileHandler, MockFileHandler>();
            var registeredObject = container.RegisteredObjects.First();
            //act
            registeredObject.GetInstance(new List<object>());
            var resolvedType = registeredObject.Instance;
            //assert
            Assert.NotNull(resolvedType);
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenGetInstanceIsCalledWithParameters_Then_InstanceIsCreated()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var container = new SimpleIocContainer.SimpleContainer();
            container.Register<IFileHandler, MockFileHandler>();
            var registeredObject = container.RegisteredObjects.First();
            var parameters = new List<object> { "test" };
            //act
            registeredObject.GetInstance(parameters);
            var resolvedType = registeredObject.Instance;
            //assert
            Assert.NotNull(resolvedType);
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }
    }
}
