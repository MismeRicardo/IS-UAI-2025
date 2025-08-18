using System.Collections.Generic;

namespace IngSoft.Services.Persistencia
{
    internal interface IConnection
    {
        void NuevaConexion(string connectionString);
        void FinalizarConexion();
        void IniciarTransaccion();
        void AceptarTransaccion();
        void CancelarTransaccion();
        void EjecutarSinResultado(string query, Dictionary<string, object> parametros);
        object EjecutarEscalar(string query, Dictionary<string, object> parametros);
        List<T> EjecutarDataTable<T>(string query, Dictionary<string, object> parametros) where T : new();
    }
}
