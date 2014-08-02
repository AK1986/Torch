using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;
using Torch.Core;


namespace Torch.UnitTests
{
    [TestClass]
    public class PowerShellDependencyTests
    {
        [TestMethod]
        public void PowerShellDependency_Default()
        {
            //Arrange
            var powerShellDependency = new PowerShellDependency();

            //Act 
            var result = powerShellDependency.Check();

           //Assert
           Assert.AreEqual(DependencyStatus.Success,result.Status);
        }
        [TestMethod]
        public void PowerShellDependency_VersionCheck()
        {
            //Arrange
            var powerShellDependency = new PowerShellDependency("2.0");

            //Act 
            var result = powerShellDependency.Check();

            //Assert
            Assert.AreEqual(DependencyStatus.Success, result.Status);
        }
        [TestMethod]
        public void PowerShellDependency_VersionCheckFail()
        {
            //Arrange
            var powerShellDependency = new PowerShellDependency("1.0");

            //Act 
            var result = powerShellDependency.Check();

            //Assert
            Assert.AreEqual(DependencyStatus.Failure, result.Status);
        }
    }
}
