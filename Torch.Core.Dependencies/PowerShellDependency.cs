using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core.Dependencies
{
    public class PowerShellDependency:IDependency
    {
        string _name;
        string _requiredVersion;

        public PowerShellDependency(string version=null)
        {
            _requiredVersion = version;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public IDepedenecyCheckResult Check()
        {
            var result = new GenericDependencyCheckResult();
            try
            {
                string version;
                if(PowershellExists(out version))
                {
                    if (version == _requiredVersion || string.IsNullOrEmpty(_requiredVersion))
                    {
                        result.Status = DependencyStatus.Success;
                    }
                    else
                    {
                        result.Status = DependencyStatus.Failure;
                        result.Message = "Powershell found but version specified was not found";
                    }
                }
                else 
                {
                    result.Status=DependencyStatus.Failure;
                }
            }
            catch (Exception ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            return result;
        }
        private bool PowershellExists(out string version)
        {
            string regval = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PowerShell\1", "Install", null).ToString();
            if (regval.Equals("1"))
            {
                version = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PowerShell\1\PowerShellEngine", "PowerShellVersion", null).ToString();
                return true;
            }
            else
            {
                version = null;
                return false;
            }
        }
    }
}
