using System;
using System.Collections.Generic;
using System.Text;

namespace Torch.Core.Dependencies
{
    public class FileDependency:IDependency
    {
        string _path;
        bool _checkRead;
        bool _checkWrite;
        bool _checkExists;
        string _name;

        public FileDependency(string path,bool checkExists=true, bool checkRead=true,bool checkWrite=false)
        {
            _path = path;
            _checkRead = checkRead;
            _checkWrite = checkWrite;
            _checkExists = checkExists;
            _name = "FileDependency: " + _path;
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
                        if (System.IO.File.Exists(_path))
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
                        var fs = System.IO.File.OpenRead(_path);
                        if (fs.CanRead)
                        {
                            result.Status = DependencyStatus.Success;
                        }
                        else
                        {
                            result.Status = DependencyStatus.Failure;
                            result.Message = "Could not read file";
                        }
                        fs.Close();
                    }
                    if (_checkWrite)
                    {
                        try
                        {
                            var fs = System.IO.File.OpenWrite(_path);
                            if (fs.CanWrite)
                            {
                                result.Status = DependencyStatus.Success;
                            }
                            fs.Close();
                        }
                        catch
                        {
                            result.Status = DependencyStatus.Failure;
                            result.Message = "Could not write to file";
                            throw;
                        }
                    }

                }
            }
            catch (System.IO.FileNotFoundException ex)
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
    }
}
