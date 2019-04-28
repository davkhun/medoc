using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocData
{
    public class Connect
    {
        private string ConnStr;
        public Connect()
        {
            ConnStr = ConfigurationManager.AppSettings["ConnStr"];
        }

        public Connect(string connStr)
        {
            ConnStr = connStr;
        }

        private T Cast<T>(object value)
        {
            if (value is T)
            {
                return (T)value;
            }
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }

        public void ExecuteNonQuery(string query, Dictionary<string,string> param = null)
        {
            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                using (var comm = new MySqlCommand(query, conn))
                {
                    comm.CommandTimeout = 80000;
                    if (param != null)
                    {
                        foreach (var p in param)
                            comm.Parameters.AddWithValue(p.Key, string.IsNullOrEmpty(p.Value)? (object)DBNull.Value: p.Value);
                    }
                    comm.ExecuteNonQuery();
                }
            }
        }

        public T ExecuteScalar<T>(string query, Dictionary<string, string> param = null)
        {
            object retVal;
            using (var conn = new MySqlConnection(ConnStr))
            {
                conn.Open();
                using (var comm = new MySqlCommand(query, conn))
                {
                    comm.CommandTimeout = 80000;
                    if (param != null)
                    {
                        foreach (var p in param)
                            comm.Parameters.AddWithValue(p.Key, string.IsNullOrEmpty(p.Value) ? (object)DBNull.Value : p.Value);
                    }
                    retVal = comm.ExecuteScalar();
                }
            }
            return Cast<T>(retVal);
        }

        public DataTable ExecuteAdapter(string query, Dictionary<string,string> param = null)
        {
            DataTable dt = new DataTable();
            using (var conn = new MySqlConnection(ConnStr))
            {
                using (var adap = new MySqlDataAdapter(query, conn))
                {
                    adap.SelectCommand.CommandTimeout = 80000;
                    if (param != null)
                    {
                        foreach (var p in param)
                            adap.SelectCommand.Parameters.AddWithValue(p.Key, string.IsNullOrEmpty(p.Value) ? (object)DBNull.Value : p.Value);
                    }
                    adap.Fill(dt);
                }
            }
            return dt;
        }
    }
}
