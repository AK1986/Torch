using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torch.Core.Interfaces
{
    public interface IComponent
    {
        string Name { get; set; }

        IEnumerable<IDependency> Dependencies { get; set; }

        bool IsRequired { get; set; }

        IComponentCheckResult Check();
    }
}
