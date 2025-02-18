using SimpleInjectionContainer;
using SimpleInjectionContainer.Contracts;
using SimpleInjectionContainerTests.Mock;
using Xunit;

namespace SimpleInjectionContainerTests.Test
{
    public class ContainerTests
    {
        [Fact]
        public void WhenRegisteredOneType_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;

            var builder = new ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>();
            var container = builder.Build();
            //act

            var resolvedType = container.Resolve<IFileHandler>();

            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }

        [Fact]
        public void WhenDependencyTypeRegistered_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockApplicationService);
            var expectedCount = 2;
            var builder = new ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>();
            builder.Register<IApplicationService, MockApplicationService>();
            var container = builder.Build();
            //act
            var mockService = container.Resolve<IApplicationService>();
            var fileHandler = container.Resolve<IFileHandler>();

            //assert
            Assert.Equal(expectedType, mockService.GetType());
            Assert.Equal(typeof(MockFileHandler), fileHandler.GetType());
            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }

        [Fact]
        public void WhenRegisterInstance_Then_ResolveContractType()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new ContainerBuilder();
            var mockFileHandler = new MockFileHandler();
            builder.Register<IFileHandler>(mockFileHandler);
            var container = builder.Build();
            //act            
            var resolvedType = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }

        [Fact]
        public void WhenRegisterInstance_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new ContainerBuilder();
            var mockFileHandler = new MockFileHandler();
            builder.Register(mockFileHandler);
            var container = builder.Build();
            //act            
            var resolvedType = container.Resolve<MockFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }

        [Fact]
        public void WhenRegisterInstance_Then_ResolveTypeSingleton()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new ContainerBuilder();
            var mockFileHandler = new MockFileHandler();
            builder.Register<IFileHandler>(mockFileHandler);
            var container = builder.Build();
            //act            
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(resolvedType, resolvedType2);
            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }

        [Fact]
        public void WhenRegisterTransient_Then_ResolveTypeTransient()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>(LifeCycleScope.Transient);
            var container = builder.Build();
            //act            
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }

        [Fact]
        public void WhenRegisterTransient_Then_ResolveTypeTransientDifferentInstances()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>(LifeCycleScope.Transient);
            var container = builder.Build();
            //act
            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.NotEqual(resolvedType, resolvedType2);

            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }

        [Fact]
        public void WhenRegisterSingleton_Then_ResolveTypeSingleton()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;
            var builder = new ContainerBuilder();
            builder.Register<IFileHandler, MockFileHandler>(LifeCycleScope.Singleton);
            var container = builder.Build();
            //act

            var resolvedType = container.Resolve<IFileHandler>();
            var resolvedType2 = container.Resolve<IFileHandler>();
            //assert
            Assert.Equal(expectedType, resolvedType.GetType());
            Assert.Equal(expectedType, resolvedType2.GetType());
            Assert.Equal(resolvedType, resolvedType2);
            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }

        [Fact]
        public void WhenDispose_Then_typesRegisteredCountZero()
        {
            //arrange            
            var expectedCount = 0;
            var builder = new ContainerBuilder();
            var container = builder.Build();
            builder.Register<MockFileHandler>();
            builder.Register<MockApplicationService>();
            //act
            builder.Dispose();
            container.Dispose();
            //assert
            Assert.Equal(expectedCount, builder.TypesRegisteredCount);
            Assert.Equal(expectedCount, container.TypesRegisteredCount);
        }
    }
}
