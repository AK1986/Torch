using System;
using System.Collections.Generic;

using System.Text;

using Torch.Core.Enums;

namespace Torch.Core.Interfaces
{
    public interface IComponentCheckResult
    {
         List<IDepedenecyCheckResult> DependencyResults { get; set; }
         ComponentStatus Status {get;set;}
    }
}
