using System;
using System.Collections.Generic;
using System.Configuration;
using IngSoft.DBConnection;
using IngSoft.DBConnection.Factory;
using IngSoft.Domain;

namespace IngSoft.Repository.Implementation
{
    public class ControlIdiomaRepository : IControlIdiomaRepository
    {
        private readonly IConnection _connection;

        public ControlIdiomaRepository(IConnection connection)
        {
            _connection = connection ?? ConnectionFactory.CreateSqlServerConnection();
        }
        public List<ControlIdioma> ObtenerControlesPorIdioma(Guid idiomaId)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
                _connection.NuevaConexion(connectionString);
                
                var parametros = new Dictionary<string, object>
                {
                    {"@IdiomaId", idiomaId }
                };

                var query = "SELECT Id, NombreControl, TextoTraducido, IdIdioma FROM ControlIdioma WHERE IdIdioma = @IdiomaId";

                var controles = _connection.EjecutarDataTable<ControlIdioma>(query, parametros);

                _connection.FinalizarConexion();

                return controles;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
