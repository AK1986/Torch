using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;
using System.Data.SqlClient;

namespace Torch.UnitTests
{
    [TestClass]
    public class SqlServerDependencyTests
    {
        [TestMethod]
        public void SqlServerDependency_Test()
        {
           //Arrange
           var connectionString=@"Data Source=.\sql2008;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=ajay@123";
           var sqlServerDependency = new SqlServerDependency(connectionString);
           var con = new SqlConnection(connectionString);
           con.Open();
            //Act
           var result = sqlServerDependency.Check();

           //Assert
           Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Success);
        }
    
        [TestMethod]
        public void SqlServerDependency_NegativeTest()
        {
            //Arrange
            var connectionString = @"Data Source=.\sql2008;Initial Catalog=master2;Persist Security Info=True;User ID=sa;Password=ajay@123";
           
            //Act
            var sqlServerDependency = new SqlServerDependency(connectionString);
            var result = sqlServerDependency.Check();

            //Assert
            Assert.IsTrue(result.Status == Core.Enums.DependencyStatus.Failure);
        }
    }
}
