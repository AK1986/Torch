using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core
{
    public class TorchBootstrapper
    {
        static IComponentFinder _componentFinder;
        static IEnumerable<IComponent> _componentList;
        static IEnumerable<ComponentCheckResult> _results;
        static string _textResults = string.Empty;
        static bool hasFailed = false;

        static TorchBootstrapper()
        {
            _componentFinder = new DynamicComponentFinder();
            _results = new List<ComponentCheckResult>();
            _textResults = "Please call VerifyDependencies() first";
        }
        public static void SetComponentFinder(IComponentFinder componentFinder)
        {
            _componentFinder = componentFinder;
        }
        public static void VerifyDependencies(bool abortOnFailure = true)
        {
            try
            {
                List<ComponentCheckResult> results = new List<ComponentCheckResult>();
                _componentList = _componentFinder.GetList();
                IComponentChecker _checker = new ComponentChecker();
                StringBuilder sb = new StringBuilder();
               

                if (_componentList != null)
                {
                    results.AddRange(_checker.CheckComponents(_componentList));
                }
                _results = results;

                foreach (var comp in _results)
                {
                    hasFailed = comp.Status != ComponentStatus.OK;
                    sb.Append(Environment.NewLine+comp.ComponentName + ": " + comp.Status.ToString());
                    foreach (var dResult in comp.DependencyResults)
                    {
                        sb.Append(Environment.NewLine);
                        sb.Append("   -");
                        sb.Append(dResult.DependencyName + " --"); 
                        sb.Append(dResult.Status.ToString());
                        if (dResult.Status != DependencyStatus.Success)
                        {
                            sb.Append(Environment.NewLine);
                            sb.Append("   -->");
                            sb.Append(dResult.Message);
                        }
                    }
                }
                _textResults = sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception while building component list", ex);
            }
        }
        public static void Continue(bool abortOnFail=true,Action<object> actionOnFail=null,object param=null)
        {
            if (hasFailed)
            {
                if (actionOnFail != null)
                {
                    actionOnFail(param);
                }
                if (abortOnFail)
                {
                    throw new Exception("Torch: Application requirements not satisfied \n" + _textResults);
                }
            }
        }
        public static string GetLog()
        {
            return _textResults;
        }
        public static IEnumerable<ComponentCheckResult> GetDetailedResult()
        {
            return _results;
        }
    }
}
