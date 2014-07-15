using System;
using System.Collections.Generic;

using System.Text;


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
