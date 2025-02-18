using SimpleInjectionContainer;
using SimpleInjectionContainer.Contracts;
using SimpleInjectionContainer.Exceptions;
using SimpleInjectionContainerTests.Mock;
using System.ComponentModel;
using Xunit;

namespace SimpleInjectionContainerTests.Test
{
    public class ExceptionTests
    {
        [Fact]
        public void WhenContainerIsEmpty_Then_ResolveTypeRaiseError()
        {
            //arrange
            var expectedCount = 0;
            var expectedMessage = "The type IFileHandler has not been registered.";
            var builder = new ContainerBuilder();
            var container = builder.Build();
            //act            
            var ex = Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<IFileHandler>());            
            //assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(expectedCount, container.typesRegisteredCount);
        }

        [Fact]
        public void WhenDependencyTypeNotRegistered_Then_ResolveTypeRaiseError()
        {
            //arrange
            var expectedCount = 1;
            var expectedMessage = "The type IFileHandler has not been registered.";
            var builder = new ContainerBuilder();
            builder.Register<IApplicationService, MockApplicationService>();
            var container = builder.Build();
            //act

            var ex = Assert.Throws<TypeNotRegisteredException>(() => container.Resolve<IApplicationService>());
            //assert
            Assert.Equal(expectedMessage, ex.Message);
            Assert.Equal(expectedCount, container.typesRegisteredCount);
        }

        [Fact]
        public void WhenRegisterNullableType_Then_RaiseException()
        {
            //arrange
            var expectedMessage = "The type Nullable`1 failed to create instance.";
            var expectedType = typeof(int?);
            var typeRegistered = new TypeRegistered(expectedType, expectedType);
            //act            
            var ex = Assert.Throws<NullInstanceException>(() => typeRegistered.GetInstance([]));
            //assert
            Assert.Equal(expectedMessage, ex.Message);
        }
    }
}