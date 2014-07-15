using System;
using System.Collections.Generic;

using System.Text;


namespace Torch.Core.Interfaces
{
    public interface IComponentListBuilder
    {
        IEnumerable<IComponent> Build();
    }
}
