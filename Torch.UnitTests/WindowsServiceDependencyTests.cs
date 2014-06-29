using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;

namespace Torch.UnitTests
{
    [TestClass]
    public class WindowsServiceDependencyTests
    {
        [TestMethod]
        public void WindowsServiceDependency_Default_Tests()
        {
            //Arrange
            //Expected is that aspnet state service is installed and running
            string serviceName = "aspnet_state";
            var winServiceDependency = new WindowsServiceDependency(serviceName);

            //Act 
            var result = winServiceDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Success);
        }
        [TestMethod]
        public void WindowsServiceDependency_MachineName_Tests()
        {
            //Arrange
            //Expected is that aspnet state service is installed and running
            string serviceName = "aspnet_state";
            var winServiceDependency = new WindowsServiceDependency(serviceName,Core.Enums.WindowsServiceStatus.Running,Environment.MachineName);

            //Act 
            var result = winServiceDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Success);
        }
        [TestMethod]
        public void WindowsServiceDependency_IsInstalled_Tests()
        {
            //Arrange
            //Expected is that aspnet state service is installed and running
            string serviceName = "aspnet_state";
            var winServiceDependency = new WindowsServiceDependency(serviceName, Core.Enums.WindowsServiceStatus.Running, Environment.MachineName,true);

            //Act 
            var result = winServiceDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Success);
        }
        [TestMethod]
        public void WindowsServiceDependency_WaitTimeOut_Tests()
        {
            //Arrange
            //Expected is that aspnet state service is installed and running
            string serviceName = "aspnet_state";
            var winServiceDependency = new WindowsServiceDependency(serviceName, Core.Enums.WindowsServiceStatus.Running, Environment.MachineName, true,2);

            //Act 
            var result = winServiceDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Success);
        }
        [TestMethod]
        public void WindowsServiceDependency_Default_NegTests()
        {
            //Arrange
            //Expected is that aspnet state service is installed and running
            string serviceName = "random_service";
            var winServiceDependency = new WindowsServiceDependency(serviceName);

            //Act 
            var result = winServiceDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Failure);
        }
        [TestMethod]
        public void WindowsServiceDependency_IsInstalled_NegTests()
        {
            //Arrange
            //Expected is that aspnet state service is installed and running
            string serviceName = "random_service";
            var winServiceDependency = new WindowsServiceDependency(serviceName,Core.Enums.WindowsServiceStatus.Any,null,true);

            //Act 
            var result = winServiceDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Failure);
        }
        [TestMethod]
        public void WindowsServiceDependency_WrongMachine_NegTests()
        {
            //Arrange
            //Expected is that aspnet state service is installed and running
            string serviceName = "aspnet_state";
            var winServiceDependency = new WindowsServiceDependency(serviceName,Core.Enums.WindowsServiceStatus.Running,"random-machine");

            //Act 
            var result = winServiceDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Failure);
        }
    }
}
