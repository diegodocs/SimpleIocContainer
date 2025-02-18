using SimpleIocContainer.UnitTest.Mock;
using SimpleIoCContainer.Contracts;
using SimpleIoCContainer.Exceptions;
using Xunit;

namespace SimpleIoCContainer.Tests.Test
{
    public class ContainerTests
    {
        [Fact]
        public void WhenContainerIsEmpty_Then_ResolveTypeRaiseError()
        {
            //arrange
            var expectedCount = 0;
            var expectedMessage = "The type IFileHandler has not been registered.";
            var builder = new SimpleIocContainer.ContainerBuilder();
            var container = builder.Build();
            //act            
            var ex = Assert.Throws<TypeNotRegisteredException>(()=>container.Resolve<IFileHandler>());

            //assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }

        [Fact]
        public void WhenRegisteredOneType_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;

            var builder = new SimpleIocContainer.ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>();
            var container = builder.Build();
            //act
            
            var resolvedType = container.Resolve<IFileHandler>();
            
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }

        [Fact]
        public void WhenDependencyTypeNotRegistered_Then_ResolveTypeRaiseError()
        {
            //arrange
            var expectedCount = 1;
            var expectedMessage = "The type IFileHandler has not been registered.";
            var builder = new SimpleIocContainer.ContainerBuilder();
            builder.Register<IApplicationService, MockApplicationService>();
            var container = builder.Build();            
            //act
            
            var ex = Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<IApplicationService>());
            //assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }

        [Fact]
        public void WhenDependencyTypeRegistered_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockApplicationService);
            var expectedCount = 2;
            var builder = new SimpleIocContainer.ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>();
            builder.Register<IApplicationService, MockApplicationService>();
            var container = builder.Build();
            //act
            var mockService = container.Resolve<IApplicationService>();
            var fileHandler = container.Resolve<IFileHandler>();

            //assert
            Assert.Equal(expectedType, mockService.GetType());
            Assert.Equal(typeof(MockFileHandler), fileHandler.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }

        [Fact]
        public void WhenRegisterInstance_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new SimpleIocContainer.ContainerBuilder();
            var mockFileHandler = new MockFileHandler();
            builder.RegisterInstance<IFileHandler>(mockFileHandler);
            var container = builder.Build();
            //act            
            var resolvedType = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }

        [Fact]
        public void WhenRegisterInstance_Then_ResolveTypeSingleton()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new SimpleIocContainer.ContainerBuilder();
            var mockFileHandler = new MockFileHandler();
            builder.RegisterInstance<IFileHandler>(mockFileHandler);
            var container = builder.Build();
            //act            
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(resolvedType, resolvedType2);
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }

        [Fact]
        public void WhenRegisterTransient_Then_ResolveTypeTransient()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new SimpleIocContainer.ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>(LifeCycleScope.Transient);
            var container = builder.Build();
            //act            
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }

        [Fact]
        public void WhenRegisterTransient_Then_ResolveTypeTransientDifferentInstances()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new SimpleIocContainer.ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>(LifeCycleScope.Transient);
            var container = builder.Build();
            //act
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.NotEqual(resolvedType, resolvedType2);
            
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }

        [Fact]
        public void WhenRegisterSingleton_Then_ResolveTypeSingleton()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new SimpleIocContainer.ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>(LifeCycleScope.Singleton);
            var container = builder.Build();
            //act
            
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(resolvedType, resolvedType2);
            Assert.Equal(expectedCount, container.RegisteredObjectsCount);
        }       
    }
}
