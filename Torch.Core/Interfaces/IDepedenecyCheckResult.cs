using System;
using System.Collections.Generic;

using System.Text;

using Torch.Core.Enums;

namespace Torch.Core.Interfaces
{
    public interface IDepedenecyCheckResult
    {
         DependencyStatus Status { get; set; }
         Exception Exception { get; set; }
         string Message { get; set; }
    }
}
