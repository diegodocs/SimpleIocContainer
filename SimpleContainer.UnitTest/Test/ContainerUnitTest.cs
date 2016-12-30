using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleIocContainer;
using System.Linq;
using SimpleIocContainer.UnitTest.Mock;

namespace SimpleContainer.UnitTest.Test
{
    [TestClass]
    public class ContainerUnitTest
    {
        [TestMethod]
        public void WhenRegisteredObjectEmpty_Then_ResolveTypeRaiseError()
        {
            //arrange
            var expectedCount = 0;
            var expectedMessage = "The type IFileHandler has not been registered.";
            var currentMessage = "";
            var container = new SimpleContainerIoc();
            //act
            try
            {
                var resolvedType = container.Resolve<IFileHandler>();
            }
            catch (Exception ex)
            {
                currentMessage = ex.Message;                
            }

            //assert
            Assert.AreEqual(expectedMessage, currentMessage);
            Assert.AreEqual(expectedCount, container.RegisteredObjects.Count());
        }

        [TestMethod]
        public void WhenRegisteredOneType_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockFileHandler);
            var expectedCount = 1;            
            
            var container = new SimpleContainerIoc();
            //act
            container.Register<IFileHandler, MockFileHandler>();
            var resolvedType = container.Resolve<IFileHandler>();
            
            //assert
            Assert.AreEqual(expectedType, resolvedType.GetType());
            Assert.AreEqual(expectedCount, container.RegisteredObjects.Count());
        }

        [TestMethod]
        public void WhenDependencyTypeNotRegistered_Then_ResolveTypeRaiseError()
        {
            //arrange
            var expectedCount = 1;
            var expectedMessage = "The type IFileHandler has not been registered.";
            var currentMessage = "";
            var container = new SimpleContainerIoc();
            //act
            container.Register<IApplicationService, MockApplicationService>();
            try
            {
                var resolvedType = container.Resolve<IApplicationService>();
            }
            catch (Exception ex)
            {
                currentMessage = ex.Message;
            }

            //assert
            Assert.AreEqual(expectedMessage, currentMessage);
            Assert.AreEqual(expectedCount, container.RegisteredObjects.Count());
        }

        [TestMethod]
        public void WhenDependencyTypeRegistered_Then_ResolveType()
        {
            //arrange
            var expectedType = typeof(MockApplicationService);
            var expectedCount = 2;

            var container = new SimpleContainerIoc();
            //act
            container.Register<IFileHandler, MockFileHandler>();
            container.Register<IApplicationService, MockApplicationService>();
            var resolvedType = container.Resolve<IApplicationService>();

            //assert
            Assert.AreEqual(expectedType, resolvedType.GetType());
            Assert.AreEqual(expectedCount, container.RegisteredObjects.Count());
        }
    }
}
