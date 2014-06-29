using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torch.Core.Enums;

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
