using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
    public class GenericDependencyCheckResult:IDepedenecyCheckResult
    {
        public DependencyStatus Status { get; set; }

        public Exception Exception { get; set; }

        public string Message { get; set; }

        public string DependencyName { get; set; }

        public GenericDependencyCheckResult(DependencyStatus status, Exception ex,string message)
        {
            Status = status;
            Exception = ex;
            Message = message;
        }
        public GenericDependencyCheckResult() { }
    }
}
