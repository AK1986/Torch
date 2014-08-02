using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
    public class ComponentCheckResult
    {
        public List<IDepedenecyCheckResult> DependencyResults { get; set; }
        public ComponentStatus Status { get; set; }
        public string ComponentName { get; set; }
        public ComponentCheckResult()
        {
            DependencyResults = new List<IDepedenecyCheckResult>();
        }
    }
}
