using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
    public class XmlFileComponentFinder:IComponentFinder
    {
        string _xmlConfigFilePath;
        public XmlFileComponentFinder(string xmlConfigFilePath)
        {
            _xmlConfigFilePath = xmlConfigFilePath;
        }
        public IEnumerable<IComponent> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
