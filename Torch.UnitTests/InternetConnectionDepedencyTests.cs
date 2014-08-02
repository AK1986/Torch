using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;
using Torch.Core;


namespace Torch.UnitTests
{
    [TestClass]
    public class InternetConnectionDepedencyTests
    {
        [TestMethod]
        public void InternetConnectionDepedency_Default()
        {
            //Arrange
            var netConnectionDepedency = new InternetConnectionDepedency();
            
            //Act 
            var result = netConnectionDepedency.Check();

            //Assert
            Assert.AreEqual(result.Status,DependencyStatus.Success);
        }
    }
}
