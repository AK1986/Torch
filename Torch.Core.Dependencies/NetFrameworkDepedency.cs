using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core.Dependencies
{
    internal class Framework
    {
        public string Name { get; set; }
        public string ServicePack { get; set; }
        public string Version { get; set; }
    }
    public class RequiredFramework
    {
        public string Version { get; set; }
        public string ReleaseVersion { get; set; }
        public int ServicePack { get; set; }
    }
   public class NetFrameworkDepedency:IDependency
    {
       RequiredFramework[] _requiredVersions;
       string _name;

       public NetFrameworkDepedency(RequiredFramework[] requiredVersions)
       {
           _requiredVersions = requiredVersions;
       }

        private List<Framework> GetVersionFromRegistry()
        {
            List<Framework> frameworks = new List<Framework>();
            // Opens the registry key for the .NET Framework entry. 
            using (RegistryKey ndpKey =
                RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").
                OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {

                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", "");
                        string sp = versionKey.GetValue("SP", "").ToString();
                        string install = versionKey.GetValue("Install", "").ToString();
                        if (install == "") //no install info, must be later.
                            Console.WriteLine(versionKeyName + "  " + name);
                        else
                        {
                            if (sp != "" && install == "1")
                            {
                                Console.WriteLine(versionKeyName + "  " + name + "  SP" + sp);
                            }

                        }
                        if (name != "")
                        {
                            string ver = versionKeyName;
                            if (name.Contains("4.5"))
                            {
                                ver = "v4.5";
                            }
                            frameworks.Add(new Framework { Version = name, ServicePack = sp, Name = ver });
                            continue;
                        }
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (name != "")
                                sp = subKey.GetValue("SP", "").ToString();
                            install = subKey.GetValue("Install", "").ToString();
                            if (install == "") //no install info, must be later.
                                Console.WriteLine(versionKeyName + "  " + name);
                            else
                            {
                                if (sp != "" && install == "1")
                                {
                                    Console.WriteLine("  " + subKeyName + "  " + name + "  SP" + sp);
                                }
                                else if (install == "1")
                                {
                                    Console.WriteLine("  " + subKeyName + "  " + name);
                                }

                            }
                                string ver = versionKeyName;
                                if (name.Contains("4.5"))
                                {
                                    ver = "v4.5";
                                }
                                frameworks.Add(new Framework { Version = name, ServicePack = sp, Name = ver+subKeyName });
                            
                        }

                    }
                }
            }
            return frameworks;

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
                var installed = GetVersionFromRegistry();
                foreach (var framework in _requiredVersions)
                {
                    bool found = false;
                    foreach (var existing in installed)
                    {
                        if (existing.Name == framework.Version)
                        {
                            if (existing.Version == framework.ReleaseVersion || string.IsNullOrEmpty(framework.ReleaseVersion))
                            {
                                if (existing.ServicePack == framework.ServicePack.ToString() || framework.ServicePack <= 0)
                                {
                                    found = true;
                                    result.Status = DependencyStatus.Success;
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        result.Status = DependencyStatus.Failure;
                        result.Message = "Framework not found: " + framework.Version + "-" + framework.ReleaseVersion;
                        break;
                    }
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
    }
}
