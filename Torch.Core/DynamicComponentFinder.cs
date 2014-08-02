using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Torch.Core
{
    public class DynamicComponentFinder : IComponentFinder
    {
        public IEnumerable<IComponent> GetList()
        {
            List<IComponent> list = new List<IComponent>();
            var types = Assembly.GetEntryAssembly().GetTypes();
            foreach (var type in types)
            {
                var interfaceTypes = type.GetInterfaces();
                if (Array.IndexOf(interfaceTypes, typeof(IComponent)) >= 0
                   && type.GetConstructor(Type.EmptyTypes) != null)
                {
                    list.Add(Activator.CreateInstance(type) as IComponent);
                }

            }
            return list;
        }
    }
}
