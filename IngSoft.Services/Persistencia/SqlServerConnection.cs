using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace IngSoft.Services.Persistencia
{
    public class SqlServerConnection : IConnection
    {
        private SqlConnection _sqlConnection;
        private SqlTransaction _oneTransaction;

        public void NuevaConexion(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
        }
        public void FinalizarConexion()
        {
            if (_sqlConnection.State != System.Data.ConnectionState.Closed)
            {
                _sqlConnection.Close();
            }
        }
        public void IniciarTransaccion()
        {
            _oneTransaction = _sqlConnection.BeginTransaction();
        }        
        public void AceptarTransaccion()
        {
            _oneTransaction.Commit();
        }

        public void CancelarTransaccion()
        {
            _oneTransaction.Rollback();
        }

        public List<T> EjecutarDataTable<T>(string query, Dictionary<string, object> parametros) where T : new()
        {
            List<T> finalResult = new List<T>();
            SqlCommand oneCommand = new SqlCommand();

            oneCommand.Connection = _sqlConnection;
            oneCommand.Transaction = _oneTransaction;
            oneCommand.CommandText = query;

            foreach (var p in parametros)
            {
                SqlParameter param = new SqlParameter();
                oneCommand.Parameters.AddWithValue(p.Key, p.Value);
            }

            oneCommand.CommandType = CommandType.Text;

            SqlDataReader resultDataReader = oneCommand.ExecuteReader();

            while (resultDataReader.Read())
            {
                T resultObject = new T();

                for (int i = 0; i <= resultDataReader.FieldCount - 1; i++)
                {
                    string columnName = resultDataReader.GetName(i);
                    object columnValue = resultDataReader[i];

                    PropertyInfo pi = resultObject.GetType().GetProperty(columnName);
                    if (pi != null)
                    {
                        pi.SetValue(resultObject, columnValue);
                    }
                }

                finalResult.Add(resultObject);

            }
            resultDataReader.Close();
            return finalResult;
        }

        public object EjecutarEscalar(string query, Dictionary<string, object> parametros)
        {
            SqlCommand oneCommand = new SqlCommand();
            oneCommand.Connection = _sqlConnection;
            oneCommand.Transaction = _oneTransaction;

            oneCommand.CommandText = query;

            foreach (var p in parametros)
            {
                oneCommand.Parameters.AddWithValue(p.Key, p.Value);
            }

            oneCommand.CommandType = CommandType.Text;
            return oneCommand.ExecuteScalar();
        }

        public void EjecutarSinResultado(string query, Dictionary<string, object> parametros)
        {
            SqlCommand oneCommand = new SqlCommand();
            oneCommand.Connection = _sqlConnection;
            oneCommand.Transaction = _oneTransaction;
            oneCommand.CommandText = query;

            foreach (var p in parametros)
            {
                oneCommand.Parameters.AddWithValue(p.Key, p.Value);
            }

            oneCommand.CommandType = CommandType.Text;
            oneCommand.ExecuteNonQuery();
        }        
    }
}
