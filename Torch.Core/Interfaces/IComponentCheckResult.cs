using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torch.Core.Enums;

namespace Torch.Core.Interfaces
{
    public interface IComponentCheckResult
    {
         List<IDepedenecyCheckResult> DependencyResults { get; set; }
         ComponentStatus Status {get;set;}
    }
}
