using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Torch.Core.Dependencies
{
    public class SqlServerDependency:IDependency
    {
        string _name;
        string _connString;

        public SqlServerDependency(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString cant be null or empty");

            _connString = connectionString;
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
                CheckDatabaseUp(_connString);
                result.Status = DependencyStatus.Success;
            }
            catch (SqlException sqlEx)
            {
                result.Status = DependencyStatus.Failure;
                result.Exception = sqlEx;
                result.Message = sqlEx.Message;
            }
            catch (Exception ex)
            {
                result.Status = DependencyStatus.Failure;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
        private void CheckDatabaseUp(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                    connection.Open();
                    var cmd = new SqlCommand("select 1", connection);
                    cmd.ExecuteScalar();
            }
        }
    }
}
