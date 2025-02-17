using SimpleIocContainer.UnitTest.Mock;
using SimpleIoCContainer.Contracts;
using SimpleIoCContainer.Exceptions;
using Xunit;

namespace SimpleContainer.UnitTest.Test
{
    public class ContainerTests
    {
        [Fact]
        public void WhenContainerIsEmpty_Then_ResolveTypeRaiseError()
        {
            //arrange
            var expectedCount = 0;
            var expectedMessage = "The type IFileHandler has not been registered.";
            
            var container = new SimpleIocContainer.SimpleContainer();
            //act            
            var ex = Assert.Throws<TypeNotRegisteredException>(()=>container.Resolve<IFileHandler>());

            //assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenRegisteredOneType_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;            
            
            var container = new SimpleIocContainer.SimpleContainer();
            //act
            container.Register<IFileHandler, MockFileHandler>();
            var resolvedType = container.Resolve<IFileHandler>();
            
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenDependencyTypeNotRegistered_Then_ResolveTypeRaiseError()
        {
            //arrange
            var expectedCount = 1;
            var expectedMessage = "The type IFileHandler has not been registered.";
            
            var container = new SimpleIocContainer.SimpleContainer();
            //act
            container.Register<IApplicationService, MockApplicationService>();
            var ex = Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<IApplicationService>());
            //assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenDependencyTypeRegistered_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockApplicationService);
            var expectedCount = 2;

            var container = new SimpleIocContainer.SimpleContainer();
            //act
            container.Register<IFileHandler, MockFileHandler>();
            container.Register<IApplicationService, MockApplicationService>();
            var mockService = container.Resolve<IApplicationService>();
            var fileHandler = container.Resolve<IFileHandler>();

            //assert
            Assert.Equal(expectedType, mockService.GetType());
            Assert.Equal(typeof(MockFileHandler), fileHandler.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenRegisterInstance_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var container = new SimpleIocContainer.SimpleContainer();
            //act
            var mockFileHandler = new MockFileHandler();
            container.RegisterInstance<IFileHandler>(mockFileHandler);
            var resolvedType = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenRegisterInstance_Then_ResolveTypeSingleton()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var container = new SimpleIocContainer.SimpleContainer();
            //act
            var mockFileHandler = new MockFileHandler();
            container.RegisterInstance<IFileHandler>(mockFileHandler);
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(resolvedType, resolvedType2);
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenRegisterTransient_Then_ResolveTypeTransient()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var container = new SimpleIocContainer.SimpleContainer();
            //act
            container.Register<IFileHandler, MockFileHandler>(ObjectLifeCycle.Transient);
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenRegisterTransient_Then_ResolveTypeTransientDifferentInstances()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var container = new SimpleIocContainer.SimpleContainer();
            //act
            container.Register<IFileHandler, MockFileHandler>(ObjectLifeCycle.Transient);
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.NotEqual(resolvedType, resolvedType2);
            
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }

        [Fact]
        public void WhenRegisterSingleton_Then_ResolveTypeSingleton()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var container = new SimpleIocContainer.SimpleContainer();
            //act
            container.Register<IFileHandler, MockFileHandler>(ObjectLifeCycle.Singleton);
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(resolvedType, resolvedType2);
            Assert.Equal(expectedCount, container.RegisteredObjects.Count);
        }       
    }
}
