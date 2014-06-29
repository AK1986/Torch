using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
