﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torch.Core.Interfaces
{
    public interface IComponentListBuilder
    {
        IEnumerable<IComponent> Build();
    }
}