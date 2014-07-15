using System;
using System.Collections.Generic;

using System.ServiceProcess;
using System.Text;

using Torch.Core.Enums;
using Torch.Core.Interfaces;

namespace Torch.Core.Dependencies
{
    public class WindowsServiceDependency : IDependency
    {
        string _name;
        string _serviceName;
        string _machineName;
        bool _checkInstalled;
        WindowsServiceStatus _expectedStatus;
        int _statusWaitTimeout;

        public WindowsServiceDependency(string serviceName, WindowsServiceStatus expectedStatus = WindowsServiceStatus.Running,
            string machineName = null,
            bool checkInstalled = false,
            int statusWaitTimeout = -1)
        {
            if (string.IsNullOrEmpty(serviceName))
                throw new ArgumentNullException("serviceName cant be null or empty");

            _name = "WindowsServiceDependency:" + serviceName;
            _serviceName = serviceName;
            _machineName = machineName;
            _checkInstalled = checkInstalled;
            _expectedStatus = expectedStatus;
            _statusWaitTimeout = statusWaitTimeout;
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
            var result = new WindowsServiceDependencyResult();
            try
            {
                if (_checkInstalled)
                {
                    bool serviceExists = false;
                    if (string.IsNullOrEmpty(_machineName))
                    {
                        foreach (var service in ServiceController.GetServices())
                        {
                            if (service.ServiceName == _serviceName)
                            {
                                serviceExists = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var service in ServiceController.GetServices(_machineName))
                        {
                            if (service.ServiceName == _serviceName)
                            {
                                serviceExists = true;
                                break;
                            }
                        }
                    }
                    if (serviceExists)
                    {
                        result.Status = DependencyStatus.Success;
                    }
                    else
                    {
                        result.Status = DependencyStatus.Failure;
                        result.Message = string.Format("Service with name {0} not found", _serviceName);
                    }
                }
                if (_expectedStatus != WindowsServiceStatus.Any || _expectedStatus != WindowsServiceStatus.Unknown)
                {
                    ServiceController sc = null;
                    if (string.IsNullOrEmpty(_machineName))
                    {
                        sc = new ServiceController(_serviceName);
                    }
                    else
                    {
                        sc = new ServiceController(_serviceName, _machineName);
                    }
                    if (_statusWaitTimeout != -1)
                    {
                        sc.WaitForStatus(getMappedStatus(_expectedStatus), new TimeSpan(0, 0, _statusWaitTimeout));
                    }
                    switch (sc.Status)
                    {
                        case ServiceControllerStatus.Running:
                            result.ActualStatus = WindowsServiceStatus.Running;
                            break;
                        case ServiceControllerStatus.Stopped:
                            result.ActualStatus = WindowsServiceStatus.Stopped;
                            break;
                        case ServiceControllerStatus.Paused:
                            result.ActualStatus = WindowsServiceStatus.Paused;
                            break;
                        case ServiceControllerStatus.StopPending:
                            result.ActualStatus = WindowsServiceStatus.Stopping;
                            break;
                        case ServiceControllerStatus.StartPending:
                            result.ActualStatus = WindowsServiceStatus.Starting;
                            break;
                        default:
                            result.ActualStatus = WindowsServiceStatus.Unknown;
                            break;
                    }
                    if (result.ActualStatus == _expectedStatus)
                    {
                        result.Status = DependencyStatus.Success;
                    }
                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            catch (System.ServiceProcess.TimeoutException ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            catch (Exception ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Message = ex.Message;
                result.Exception = ex;
            }

            return result;
        }
        private ServiceControllerStatus getMappedStatus(WindowsServiceStatus status)
        {
            ServiceControllerStatus result = default(ServiceControllerStatus);

            switch (status)
            {
                case WindowsServiceStatus.Paused:
                    result = ServiceControllerStatus.Paused;
                    break;
                case WindowsServiceStatus.Running:
                    result = ServiceControllerStatus.Running;
                    break;
                case WindowsServiceStatus.Starting:
                    result = ServiceControllerStatus.StartPending;
                    break;
                case WindowsServiceStatus.Stopped:
                    result = ServiceControllerStatus.Stopped;
                    break;
                case WindowsServiceStatus.Stopping:
                    result = ServiceControllerStatus.StopPending;
                    break;
            }

            return result;
        }
    }
}
