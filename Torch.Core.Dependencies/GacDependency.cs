using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Torch.Core.Interfaces;

namespace Torch.Core.Dependencies
{
    public class GacDependency:IDependency
    {
        string _name;
        string _assemblyName;

        public GacDependency(string assemblyName,ProcessorArchitecture processorArchitecture=ProcessorArchitecture.None)
        {
            if (string.IsNullOrEmpty(assemblyName))
                throw new ArgumentNullException("assemblyName cant be null or empty");

            _assemblyName = assemblyName;
            if (!_assemblyName.Contains("processorArchitecture"))
            {
                _assemblyName = _assemblyName + ",processorArchitecture=" + processorArchitecture.ToString();
            }
            _name = "GacDependency: " + assemblyName;
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
                bool inGac = Assembly.ReflectionOnlyLoad(_assemblyName).GlobalAssemblyCache;
                if (!inGac)
                {
                    //try by COM interop once more
                    inGac = GacUtil.IsAssemblyInGAC(_assemblyName);
                }
                if (inGac)
                {
                    result.Status = Enums.DependencyStatus.Success;
                }
                else
                {
                    result.Status = Enums.DependencyStatus.Failure;
                    result.Message = "Assembly not found in GAC, please check assembly name ,use processorArchitecture param";
                }
            }
            catch (Exception ex)
            {
                result.Status = Enums.DependencyStatus.Failure;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            return result;
        }
    }

    public static class GacUtil
    {
        [DllImport("fusion.dll")]
        private static extern IntPtr CreateAssemblyCache(
            out IAssemblyCache ppAsmCache,
            int reserved);

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")]
        private interface IAssemblyCache
        {
            int Dummy1();

            [PreserveSig()]
            IntPtr QueryAssemblyInfo(
                int flags,
                [MarshalAs(UnmanagedType.LPWStr)] string assemblyName,
                ref AssemblyInfo assemblyInfo);

            int Dummy2();
            int Dummy3();
            int Dummy4();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AssemblyInfo
        {
            public int cbAssemblyInfo;
            public int assemblyFlags;
            public long assemblySizeInKB;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string currentAssemblyPath;

            public int cchBuf;
        }

        public static bool IsAssemblyInGAC(string assemblyName)
        {
            var assembyInfo = new AssemblyInfo { cchBuf = 512 };
            assembyInfo.currentAssemblyPath = new string('\0', assembyInfo.cchBuf);

            IAssemblyCache assemblyCache;

            var hr = CreateAssemblyCache(out assemblyCache, 0);

            if (hr == IntPtr.Zero)
            {
                hr = assemblyCache.QueryAssemblyInfo(
                    1,
                    assemblyName,
                    ref assembyInfo);

                if (hr != IntPtr.Zero)
                {
                    return false;
                }

                return true;
            }

            Marshal.ThrowExceptionForHR(hr.ToInt32());
            return false;
        }
    }
}
