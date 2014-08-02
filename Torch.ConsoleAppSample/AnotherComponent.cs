using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torch.Core;
using Torch.Core.Dependencies;


namespace Torch.ConsoleAppSample
{
    public class AnotherComponent:IComponent
    {
        public string Name
        {
            get { return "Another Component";}
        }

        public IEnumerable<IDependency> GetDependencies()
        {
            var dirDerpendency = new DirectoryDependency(@"C:\Program Files\Mozilla Firefox", checkExists: true);
            var pingDependency = new PingDependency("127.0.0.1");
            var depenecyList = new List<IDependency>();
            depenecyList.Add(dirDerpendency);
            depenecyList.Add(pingDependency);
            return depenecyList;
        }

        public bool IsRequired
        {
            get { return true ;}
        }

        public void DoSomething()
        {

        }
    }
}
