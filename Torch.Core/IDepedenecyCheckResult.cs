using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
    public interface IDepedenecyCheckResult
    {
         DependencyStatus Status { get; set; }
         Exception Exception { get; set; }
         string Message { get; set; }
         string DependencyName { get; set; }
    }
}
