using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
    public class ComponentChecker : IComponentChecker
    {
        public List<ComponentCheckResult> CheckComponents(IEnumerable<IComponent> components)
        {
            var list = new List<ComponentCheckResult>();
            try
            {  
                foreach (var component in components)
            	{
                    var result = new ComponentCheckResult();
                    result.ComponentName = component.Name;
                    foreach (var item in component.GetDependencies())
                    {
                        try
                        {
                            var dependencyResult = item.Check();
                            dependencyResult.DependencyName = item.Name;
                            result.DependencyResults.Add(dependencyResult);
                            result.Status = dependencyResult.Status == DependencyStatus.Success ? ComponentStatus.OK : ComponentStatus.Failed;
                            if (dependencyResult.Status != DependencyStatus.Success)
                            {
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            result.Status = ComponentStatus.FailedDueToError;
                        }
                    }
                    list.Add(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception while checking components",ex);
            }
            return list;
        }
    }
}
