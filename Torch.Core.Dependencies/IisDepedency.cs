using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core.Dependencies
{
    public class IisDepedency:IDependency
    {
        string _name;
        bool _isRunning;
        string _machineName;
        public IisDepedency(bool isRunning = true, string machineName = null)
        {
            _name = "IIS Depedency";
            _isRunning = isRunning;
            _machineName = machineName;
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
            WindowsServiceStatus status = _isRunning?WindowsServiceStatus.Running:WindowsServiceStatus.Any;
            return new WindowsServiceDependency("W3SVC", status, _machineName, true).Check();
        }
    }
}
