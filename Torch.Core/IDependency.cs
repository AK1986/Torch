using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
   public interface IDependency
    {
         string Name { get; set; }  
         IDepedenecyCheckResult Check();
    }
}
