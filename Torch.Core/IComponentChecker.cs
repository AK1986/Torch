using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
   public interface IComponentChecker
    {
       List<ComponentCheckResult> CheckComponents(IEnumerable<IComponent> components);
    }
}
