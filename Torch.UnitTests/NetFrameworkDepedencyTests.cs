using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;
using Torch.Core;


namespace Torch.UnitTests
{
    [TestClass]
    public class NetFrameworkDepedencyTests
    {
        [TestMethod]
        public void NetFrameworkDepedency_Default()
        {
            //Arrangene
            var net20 = new RequiredFramework{Version=FrameworkVersion.Net20};
            var netFrwDependency = new NetFrameworkDepedency(new RequiredFramework[] { net20 });

            //Act 
            var result = netFrwDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Success);
        }

        [TestMethod]
        public void NetFrameworkDepedency_ReleaseVersionNotFound()
        {
            //Arrangene
            var net20 = new RequiredFramework { Version = FrameworkVersion.Net20,ReleaseVersion="non-existent" };
            var netFrwDependency = new NetFrameworkDepedency(new RequiredFramework[] { net20 });

            //Act 
            var result = netFrwDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Failure);
            Assert.IsNotNull(result.Message);
        }
        [TestMethod]
        public void NetFrameworkDepedency_ServicePackNotFound()
        {
            //Arrangene
            var net20Sp3 = new RequiredFramework { Version = FrameworkVersion.Net20, ServicePack=3 };
            var netFrwDependency = new NetFrameworkDepedency(new RequiredFramework[] { net20Sp3 });

            //Act 
            var result = netFrwDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Failure);
            Assert.IsNotNull(result.Message);
        }
        [TestMethod]
        public void NetFrameworkDepedency_ServicePackFound()
        {
            //Arrangene
            var net20Sp2 = new RequiredFramework { Version = FrameworkVersion.Net20, ServicePack = 2 };
            var netFrwDependency = new NetFrameworkDepedency(new RequiredFramework[] { net20Sp2 });

            //Act 
            var result = netFrwDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Success);
        }
        [TestMethod]
        public void NetFrameworkDepedency_MultipleSuccess()
        {
            //Arrangene
            var net20Sp2 = new RequiredFramework { Version = FrameworkVersion.Net20, ServicePack = 2 };
            var net35 = new RequiredFramework { Version = FrameworkVersion.Net35 };
            var netFrwDependency = new NetFrameworkDepedency(new RequiredFramework[] { net20Sp2,net35 });

            //Act 
            var result = netFrwDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Success);
        }
        [TestMethod]
        public void NetFrameworkDepedency_MultipleFail()
        {
            //Arrangene
            var net20Sp2 = new RequiredFramework { Version = FrameworkVersion.Net20};
            var net35 = new RequiredFramework { Version = FrameworkVersion.Net35 ,ReleaseVersion="random"};
            var netFrwDependency = new NetFrameworkDepedency(new RequiredFramework[] { net20Sp2, net35 });

            //Act 
            var result = netFrwDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Failure);
            Assert.IsNotNull(result.Message);
        }
    }
}
