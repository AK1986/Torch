using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
    public interface IComponent
    {
        string Name { get;}

        IEnumerable<IDependency> GetDependencies();

        bool IsRequired { get;}
    }
}
