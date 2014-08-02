using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core.Dependencies
{
    public class WindowsServiceDependencyResult : GenericDependencyCheckResult
    {
        public WindowsServiceDependencyResult()
        {
        }
        public WindowsServiceDependencyResult(DependencyStatus status,Exception ex,string message):base(status,ex,message)
        {

        }
        public WindowsServiceStatus ActualStatus { get; set; }
    }
}
