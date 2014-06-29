using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Torch.Core.Interfaces;

namespace Torch.Core.Dependencies
{
    public class PortDepedency:IDependency
    {
        string _name;
        int _port;

        public PortDepedency(int port)
        {
            if (port<=0)
                throw new ArgumentNullException("Port cannot be zero or negative");
            _port = port;
            _name = "Port:" + _port + " Dependency";
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
                if (!PortInUse(_port))
                {
                    result.Status = Enums.DependencyStatus.Success;
                }
                else
                {
                    result.Status = Enums.DependencyStatus.Failure;
                }
            }
            catch (Exception ex)
            {
                result.Status = Enums.DependencyStatus.Failure;
                result.Message = "Error while checking port in use";
                result.Exception = ex;
            }

            return result;
        }
        public bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }
    }
}
