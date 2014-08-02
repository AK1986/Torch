using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
    public interface IComponentFinder
    {
        IEnumerable<IComponent> GetList();
    }
}
