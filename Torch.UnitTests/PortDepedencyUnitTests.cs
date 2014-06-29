using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;
using System.Net;
using System.Net.Sockets;

namespace Torch.UnitTests
{
    [TestClass]
    public class PortDepedencyUnitTests
    {
        [TestMethod]
        public void PortDepedency_Default_Test()
        {
            //Arrange
            int port = 8080;
            var portDependency = new PortDepedency(port);

            //Act 
            var result = portDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Success);
        }
        [TestMethod]
        public void PortDepedency_Neg_Test()
        {
            //Arrange
            int port = 8080;
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"),port);
            tcpListener.Start();
            var portDependency = new PortDepedency(port);

            //Act 
            var result = portDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Failure);
        }
    }
}
