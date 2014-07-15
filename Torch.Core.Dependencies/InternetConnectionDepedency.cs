using System;
using System.Collections.Generic;

using System.Text;

using Torch.Core.Enums;
using Torch.Core.Interfaces;

namespace Torch.Core.Dependencies
{
    public class InternetConnectionDepedency : IDependency
    {
        string _name;
        int _timeout;
        public InternetConnectionDepedency(int timeoutInSeconds = 10)
        {
            _timeout = timeoutInSeconds;
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public IDepedenecyCheckResult Check()
        {
            var result = new GenericDependencyCheckResult();
            try
            {
                var google = new WebResourceDependency("http://www.google.com", _timeout);
                var yahoo = new WebResourceDependency("http://www.yahoo.com", _timeout);
                var microsoft = new WebResourceDependency("http://www.microsoft.com", _timeout);
                bool success = google.Check().Status == DependencyStatus.Success &&
                              yahoo.Check().Status == DependencyStatus.Success &&
                              microsoft.Check().Status == DependencyStatus.Success;

                result.Status = success ? DependencyStatus.Success : DependencyStatus.Failure;
            }
            catch (Exception ex)
            {
                result.Status = Enums.DependencyStatus.Failure;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            return result;

        }
    }
}
