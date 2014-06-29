using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;

namespace Torch.UnitTests
{
    [TestClass]
    public class GacDependencyTests
    {
        [TestMethod]
        public void GacDependency_Test()
        {
            //Arrange 
            Type type = typeof(System.String);
            string assemblyName = type.Assembly.FullName.ToString();
            var gacDependency = new GacDependency(assemblyName,System.Reflection.ProcessorArchitecture.X86);

            //Act 
            var result = gacDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Success);
        }

        [TestMethod]
        public void GacDependency_NegTest()
        {
            //Arrange 
            Type type = typeof(Torch.Core.Dependencies.GacDependency);
            string assemblyName = type.Assembly.FullName.ToString();
            var gacDependency = new GacDependency(assemblyName, System.Reflection.ProcessorArchitecture.MSIL);

            //Act 
            var result = gacDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Failure);
        }
    }
}
