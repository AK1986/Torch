using System;
using System.Collections.Generic;

using System.Text;


namespace Torch.Core.Interfaces
{
   public interface IComponentChecker
    {
       List<IComponentCheckResult> CheckComponents(IEnumerable<IComponent> components);
    }
}
