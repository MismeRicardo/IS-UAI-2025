using System;
using System.Collections.Generic;
using System.Linq;
using IngSoft.Domain;
using IngSoft.DBConnection;
using IngSoft.DBConnection.Factory;


namespace IngSoft.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConnection _connection;

        public UsuarioRepository(IConnection connection)
        {
            _connection = connection ?? ConnectionFactory.CreateSqlServerConnection();
        }

        public void GuardarUsuario(Usuario usuario)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
            _connection.NuevaConexion(connectionString);
            try
            {
                _connection.IniciarTransaccion();
                var parametros = new Dictionary<string, object>
                {
                    {"@Id", usuario.Id},
                    {"@Nombre", usuario.Nombre },
                    {"@Apellido", usuario.Apellido },
                    {"@Email", usuario.Email },
                    {"@Contrasena", usuario.Contrasena },
                    {"@UserName", usuario.UserName }
                };
                _connection.EjecutarSinResultado("INSERT INTO Usuario (Id, Nombre, Apellido, Email, Contrasena, UserName) VALUES (@Id, @Nombre, @Apellido, @Email, @Contrasena, @UserName)", parametros);
            }
            catch (Exception)
            {
                _connection.CancelarTransaccion();
                throw;
            }
            _connection.AceptarTransaccion();
            _connection.FinalizarConexion();
        }
        public List<Usuario> ObtenerUsuarios()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
            _connection.NuevaConexion(connectionString);
            var query = "SELECT Id, Nombre, Apellido, Email, Contrasena, UserName FROM Usuario";
            var usuarios = _connection.EjecutarDataTable<Usuario>(query, new Dictionary<string, object>());
            _connection.FinalizarConexion();
            return usuarios;
        }

        public List<Usuario> ObtenerUsuariosFiltrados(string filtro)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
            _connection.NuevaConexion(connectionString);
            var query = "SELECT Id, Nombre, Apellido, Email, Contrasena, UserName FROM Usuario WHERE Nombre LIKE @Filtro OR Apellido LIKE @Filtro OR Email LIKE @Filtro OR UserName LIKE @Filtro";
            var parametros = new Dictionary<string, object>
            {
                {"@Filtro", $"%{filtro}%"}
            };
            var usuarios = _connection.EjecutarDataTable<Usuario>(query, parametros);
            _connection.FinalizarConexion();
            return usuarios;
        }
    }
}
