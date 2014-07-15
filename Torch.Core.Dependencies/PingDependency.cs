using System;
using System.Collections.Generic;

using System.Net;
using System.Net.NetworkInformation;
using System.Text;

using Torch.Core.Enums;
using Torch.Core.Interfaces;

namespace Torch.Core.Dependencies
{
     public class PingDependency:IDependency
    {
         string _name;
         string _machineNameOrIpadress;
         bool _isIpAddress;
         IPAddress ipAddress;

         public PingDependency(string machineNameOrIpadress)
         {
             if (string.IsNullOrEmpty(machineNameOrIpadress))
                 throw new ArgumentNullException("machineNameOrIpadress cant be null or empty");

             _isIpAddress = IPAddress.TryParse(machineNameOrIpadress, out ipAddress);

             _machineNameOrIpadress = machineNameOrIpadress;
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
               var pingReply = _isIpAddress ? new Ping().Send(ipAddress) : new Ping().Send(_machineNameOrIpadress);
               result.Status = pingReply.Status == IPStatus.Success ? DependencyStatus.Success : DependencyStatus.Failure;
               result.Message = result.Status == DependencyStatus.Failure ? pingReply.Status.ToString() : "";
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
}
