using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace Torch.Core.Dependencies
{
   public class DirectoryDependency:IDependency
    {
        string _path;
        bool _checkRead;
        bool _checkWrite;
        bool _checkExists;
        string _name;

        public DirectoryDependency(string path, bool checkExists = true, bool checkRead = true, bool checkWrite = false)
        {
            _path = path;
            _checkRead = checkRead;
            _checkWrite = checkWrite;
            _checkExists = checkExists;
            _name = "DirectoryDependency: " + _path;
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
                if (!string.IsNullOrEmpty(_path))
                {
                    if (_checkExists)
                    {
                        if (System.IO.Directory.Exists(_path))
                        {
                            result.Status = DependencyStatus.Success;
                        }
                        else
                        {
                            result.Message = "File does not exists";
                            result.Status = DependencyStatus.Failure;
                        }
                    }
                    if (_checkRead)
                    {
                        if (CanRead(_path))
                        {
                            result.Status = DependencyStatus.Success;
                        }
                        else
                        {
                            result.Message = "Directory does not have read access";
                            result.Status = DependencyStatus.Failure;
                        }
                    }
                    if (_checkWrite)
                    {
                        if(CanWrite(_path))
                        {
                            result.Status = DependencyStatus.Success;
                        }
                        else
                        {
                            result.Status = DependencyStatus.Failure;
                            result.Message = "Directory does have write access";
                        }
                    }
                }
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Message = "File not found exception occured";
                result.Exception = ex;
            }
            catch (System.IO.IOException ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Message = "File I/O exception occured";
                result.Exception = ex;
            }
            catch (System.Security.SecurityException ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Message = "Security exception occured";
                result.Exception = ex;
            }
            catch (Exception ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Message = "Unknown exception occured";
                result.Exception = ex;
            }
            return result;
        }


        private bool CanWrite(string path)
        {
            var writeAllow = false;
            var writeDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true,
                                        typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write)
                    continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = true;
            }

            return writeAllow && !writeDeny;
        }

        private bool CanRead(string path)
        {
            var readAllow = false;
            var readDeny = false;
            var accessControlList = Directory.GetAccessControl(path);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            foreach (FileSystemAccessRule rule in accessRules)
            {
                if ((FileSystemRights.Read & rule.FileSystemRights) != FileSystemRights.Read) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    readAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    readDeny = true;
            }

            return readAllow && !readDeny;
        }
    }
}
