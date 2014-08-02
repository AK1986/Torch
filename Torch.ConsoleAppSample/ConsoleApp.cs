using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torch.Core;
using Torch.Core.Dependencies;


namespace Torch.ConsoleAppSample
{
    public class ConsoleApp:IComponent
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting app..");
            TorchBootstrapper.SetComponentFinder(new DynamicComponentFinder());
            TorchBootstrapper.VerifyDependencies();
            Console.WriteLine(TorchBootstrapper.GetLog());
            TorchBootstrapper.Continue();
            Console.Read();
        }
        #region Torch.IComponent members
        public string Name
        {
            get
            {
              return "My Console Application";
            }
        }
        public IEnumerable<IDependency> GetDependencies()
        {
           var fileDependency = new FileDependency(@"C:\Windows\System32\drivers\etc\hosts",checkExists:true);
           var internetConnectionDepedency = new InternetConnectionDepedency(timeoutInSeconds: 15);
           var iisDepedency = new IisDepedency();
           var depenecyList = new List<IDependency>();
           depenecyList.Add(fileDependency);
           depenecyList.Add(internetConnectionDepedency);
           depenecyList.Add(iisDepedency);
           return depenecyList;
        }
        public bool IsRequired
        {
            get
            {
                return true;
            }
        }
        #endregion
    }
}

