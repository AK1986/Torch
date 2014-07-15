using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Torch.Core.Enums;
using Torch.Core.Interfaces;

namespace Torch.Core.Dependencies
{
    public class WebResourceDependency:IDependency
    {
        string _name;
        string _url;
        int _timeout;
        Predicate<string> predicate;
        public WebResourceDependency(string url, int timeoutInSeconds = 2, Predicate<string> contains = null)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url can not be null or empty");
            if(!Uri.IsWellFormedUriString(url,UriKind.Absolute))
                throw new ArgumentException("url is not valid");
            _url = url;
            _timeout = timeoutInSeconds;
            predicate = contains;
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
                var request = WebRequest.Create(_url);
                request.Timeout = _timeout*1000;
                var response =request.GetResponse() as HttpWebResponse;
                if (response != null)
                {
                    if (predicate == null)
                    {
                        result.Status = response.StatusCode == HttpStatusCode.OK ? DependencyStatus.Success : DependencyStatus.Failure;
                    }
                    else
                    {
                        var streamReader = new StreamReader(response.GetResponseStream());
                        var content = streamReader.ReadToEnd();
                        if (predicate(content))
                        {
                            result.Status = DependencyStatus.Success;
                        }
                        else
                        {
                            result.Status = DependencyStatus.Failure;
                            result.Message = "Expected content not found in web resource";
                        }
                    }
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
}
