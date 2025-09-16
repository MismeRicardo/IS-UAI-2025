using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using IngSoft.DBConnection;
using IngSoft.DBConnection.Factory;
using IngSoft.Domain;

namespace IngSoft.Repository.Implementation
{
    public class IdiomaRepository: IIdiomaRepository
    {
        private readonly IConnection _connection;
        public IdiomaRepository(IConnection connection)
        {
            _connection = connection ?? ConnectionFactory.CreateSqlServerConnection();
        }

        public void CrearIdioma(Idioma idioma)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
            _connection.NuevaConexion(connectionString);
            var query = "INSERT INTO Idioma (Id, Nombre, Codigo, IsDefault) VALUES (@Id, @Nombre, @Codigo, @IsDefault)";
            var parameters = new Dictionary<string, object>
            {
                {"@Id", idioma.Id},
                {"@Nombre", idioma.Nombre},
                {"@Codigo", idioma.Codigo},
                {"@IsDefault", idioma.isDefault ? 1 : 0}
            };
            _connection.EjecutarSinResultado(query, parameters);
            _connection.FinalizarConexion();
        }

        public Idioma ObtenerIdiomaPorDefecto()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
            _connection.NuevaConexion(connectionString);
            var query = "SELECT Id, Nombre, Codigo, IsDefault FROM Idioma WHERE isDefault = 1";
            var idioma = _connection.EjecutarDataTable<Idioma>(query, new Dictionary<string, object>());
            _connection.FinalizarConexion();

            return idioma.FirstOrDefault();
        }

        public List<Idioma> ObtenerIdiomas()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
                _connection.NuevaConexion(connectionString);
                
                var query = "SELECT Id, Nombre, Codigo FROM Idioma";
                var idiomas = _connection.EjecutarDataTable<Idioma>(query, new Dictionary<string, object>());
                
                _connection.FinalizarConexion();
                
                return idiomas;                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
