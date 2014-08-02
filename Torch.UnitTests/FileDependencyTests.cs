using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;

namespace Torch.UnitTests
{
    [TestClass]
    public class FileDependencyTests
    {
        [TestMethod]
        public void FileDependency_Exists_UnitTests()
        {
            //Arrange 
            var file = "torchtest" + Guid.NewGuid().ToString()+".txt";
            var fs= System.IO.File.Create(file);
            fs.Close();
            bool exists = System.IO.File.Exists(file);

            //Act 
            var fileDependency = new FileDependency(file,true);
            var result= fileDependency.Check();
          
            //Assert
            Assert.AreEqual(result.Status == Core.DependencyStatus.Success, exists);
            Assert.IsNull(result.Exception);

            //clean up
            TestUtils.CleanupCode(() => {
                System.IO.File.Delete(file);
            });
            
        }
        [TestMethod]
        public void FileDependency_CheckRead_UnitTests()
        {
            //Arrange 
            var file = "torchtest" + Guid.NewGuid().ToString() + ".txt";
            var fs = System.IO.File.Create(file);
            bool canRead = fs.CanRead;
            fs.Close();

            //Act 
            var fileDependency = new FileDependency(file, true, true);
            var result = fileDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.DependencyStatus.Success, canRead);
            Assert.IsNull(result.Exception);

            //clean up
            TestUtils.CleanupCode(() =>
            {
                System.IO.File.Delete(file);
            });
        }
        
        [TestMethod]
        public void FileDependency_CheckWrite_UnitTests()
        {
            //Arrange 
            var file = "torchtest" + Guid.NewGuid().ToString() + ".txt";
            var fs = System.IO.File.Create(file);
            var sw = new System.IO.StreamWriter(fs);
            sw.Write("torchTest");
            sw.Close();
            fs.Close();
            var canWrite = true;
            
           //Act 
            var fileDependency = new FileDependency(file, true, false,true);
            var result = fileDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.DependencyStatus.Success, canWrite);
            Assert.IsNull(result.Exception);

            //clean up
            TestUtils.CleanupCode(() =>
            {
                System.IO.File.Delete(file);
            });
        }
        [TestMethod]
        public void FileDependency_Exists_Neg_UnitTests()
        {
            //Arrange 
            var file = "torchtest" + Guid.NewGuid().ToString()+".txt";

            //Act 
            var fileDependency = new FileDependency(file,true);
            var result= fileDependency.Check();
          
            //Assert
            Assert.AreEqual(result.Status == Core.DependencyStatus.Failure, true);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public void FileDependency_CanRead_Neg_UnitTests()
        {
            //Arrange 
            var file = "torchtest" + Guid.NewGuid().ToString() + ".txt";

            //Act 
            var fileDependency = new FileDependency(file, true,true);
            var result = fileDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.DependencyStatus.Failure, true);
            Assert.IsNotNull(result.Exception);
        }

        [TestMethod]
        public void FileDependency_CanWrite_Neg_UnitTests()
        {
            //Arrange 
            var file = "torchtest" + Guid.NewGuid().ToString() + ".txt";
            var fs = System.IO.File.Create(file);
            var sw = new System.IO.StreamWriter(fs);
            //dont close stream so that write fails

            //Act 
            var fileDependency = new FileDependency(file, true,false,true);
            var result = fileDependency.Check();

            //Assert
            Assert.AreEqual(result.Status == Core.DependencyStatus.Failure, true);
            Assert.IsNotNull(result.Exception);
        }
    }
}
