using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;
using System.Security.AccessControl;
using System.IO;

namespace Torch.UnitTests
{
    [TestClass]
    public class DirectoryDependencyTests
    {
        [TestMethod]
        public void DirectoryDependency_Exists_UnitTests()
        {
            //Arrange 
            var dir = "torch" + Guid.NewGuid().ToString().Replace("-" ,"");
            System.IO.Directory.CreateDirectory(dir);
            bool exists = System.IO.Directory.Exists(dir);

            //Act 
            var directoryDependency = new DirectoryDependency(dir, true);
            var result = directoryDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.Enums.DependencyStatus.Success, exists);
            Assert.IsNull(result.Exception);

            //clean up
            TestUtils.CleanupCode(() =>
            {
                System.IO.Directory.Delete(dir);
            });

        }

        [TestMethod]
        public void DirectoryDependency_CheckRead_UnitTests()
        {
            //Arrange 
            var dir = "torch" + Guid.NewGuid().ToString().Replace("-" ,"");
            var userName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Read, AccessControlType.Allow));
            securityRules.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.FullControl, AccessControlType.Allow));
            Directory.CreateDirectory(dir, securityRules);
            bool canRead = true;

            //Act 
            var directoryDependency = new DirectoryDependency(dir, true, true);
            var result = directoryDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.Enums.DependencyStatus.Success, canRead);
            Assert.IsNull(result.Exception);

            //clean up
            TestUtils.CleanupCode(() =>
            {
                System.IO.Directory.Delete(dir);
            });
        }

        [TestMethod]
        public void DirectoryDependency_CheckWrite_UnitTests()
        {
            //Arrange 
            var dir = "torch" + Guid.NewGuid().ToString().Replace("-", "");
            var userName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Read, AccessControlType.Allow));
            securityRules.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.FullControl, AccessControlType.Allow));
            Directory.CreateDirectory(dir, securityRules);
            bool canWrite = true;

            //Act 
            var directoryDependency = new DirectoryDependency(dir, true, false, true);
            var result = directoryDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.Enums.DependencyStatus.Success, canWrite);
            Assert.IsNull(result.Exception);

            //clean up
            TestUtils.CleanupCode(() =>
            {
                System.IO.Directory.Delete(dir);
            });
        }
        [TestMethod]
        public void DirectoryDependency_Exists_Neg_UnitTests()
        {
            //Arrange 
             var dir = "torch" + Guid.NewGuid().ToString().Replace("-" ,"");

            //Act 
            var directoryDependency = new DirectoryDependency(dir, true);
            var result = directoryDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.Enums.DependencyStatus.Failure, true);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public void DirectoryDependency_CanRead_Neg_UnitTests()
        {
            //Arrange 
            var dir = "torch" + Guid.NewGuid().ToString().Replace("-", "");
            var userName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Read, AccessControlType.Deny));
            securityRules.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.FullControl, AccessControlType.Deny));
            Directory.CreateDirectory(dir, securityRules);

            //Act 
            var directoryDependency = new DirectoryDependency(dir, true, true);
            var result = directoryDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.Enums.DependencyStatus.Failure, true);
        }

        [TestMethod]
        public void DirectoryDependency_CanWrite_Neg_UnitTests()
        {
            //Arrange 
            var dir = "torch" + Guid.NewGuid().ToString().Replace("-", "");
            var userName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Read, AccessControlType.Deny));
            securityRules.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.FullControl, AccessControlType.Deny));
            Directory.CreateDirectory(dir, securityRules);

            //Act 
            var directoryDependency = new DirectoryDependency(dir, true, false, true);
            var result = directoryDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.Enums.DependencyStatus.Failure, true);
        }
    }
}
