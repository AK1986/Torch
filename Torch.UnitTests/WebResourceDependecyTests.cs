using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torch.Core.Dependencies;
using Torch.Core;


namespace Torch.UnitTests
{
    [TestClass]
    public class WebResourceDependecyTests
    {
        [TestMethod]
        public void WebResourceDependecy_Defualt()
        {
            //Arrange
            var webResourceDependency = new WebResourceDependency("https://www.flickr.com/photos/bhala/11946319853/in/pool-best100only");

            //Act 
            var result = webResourceDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Success);
        }
        [TestMethod]
        public void WebResourceDependecy_Fail()
        {
            //Arrange
            var webResourceDependency = new WebResourceDependency("https://www.flickr.com/random.png");

            //Act 
            var result = webResourceDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Failure);
        }
        [TestMethod]
        public void WebResourceDependecy_ContentPredicate()
        {
            //Arrange)
            var predicate = new Predicate<string>((content) => { return content.Contains("GetCitiesByCountry");});
            var webResourceDependency = new WebResourceDependency("http://www.webservicex.net/globalweather.asmx",contains:predicate);

            //Act 
            var result = webResourceDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Success);
        }
        [TestMethod]
        public void WebResourceDependecy_ContentPredicat_Fail()
        {
            //Arrange)
            var predicate = new Predicate<string>((content) => { return content.Contains("random-xyz"); });
            var webResourceDependency = new WebResourceDependency("http://www.webservicex.net/globalweather.asmx", contains: predicate);

            //Act 
            var result = webResourceDependency.Check();

            //Assert
            Assert.AreEqual(result.Status, DependencyStatus.Failure);
            Assert.IsNotNull(result.Message);
        }
    }
}
