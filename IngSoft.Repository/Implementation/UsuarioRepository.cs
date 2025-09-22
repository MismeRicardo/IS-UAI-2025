using System;
using System.Collections.Generic;
using System.Linq;
using IngSoft.Domain;
using IngSoft.DBConnection;
using IngSoft.DBConnection.Factory;
using IngSoft.Services.Encriptadores;

namespace IngSoft.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConnection _connection;
        private static readonly string connectionString= System.Configuration.ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;

        internal UsuarioRepository(IConnection connection)
        {
            _connection = connection ?? ConnectionFactory.CreateSqlServerConnection();
        }

        public void GuardarUsuario(Usuario usuario)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
            EncriptadorExperto mEncritpador = new EncriptadorExperto();
            usuario.Contrasena = mEncritpador.EncriptadorSecuencial(usuario.Contrasena);
            _connection.NuevaConexion(connectionString);
            try
            {
                _connection.IniciarTransaccion();
               usuario.Id = _connection.ObtenerUltimoId("Usuario", "Id")+1;
                var parametros = new Dictionary<string, object>
                {
                    {"@Id", usuario.Id},
                    {"@Nombre", usuario.Nombre },
                    {"@Apellido", usuario.Apellido },
                    {"@Email", usuario.Email },
                    {"@Contrasena", usuario.Contrasena },
                    {"@UserName", usuario.UserName },
                    {"@Bloqueado", 0},
                    {"@CantidadIntentos", 0}

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
        public Usuario ObtenerUsuario(Usuario pUsuario)
        {
            _connection.NuevaConexion(connectionString);
            string query = "SELECT Id, Nombre, Apellido, Email, Contrasena, Username, Bloqueado, CantidadIntentos FROM Usuario WHERE Username = @Username";
            EncriptadorExperto encriptadorExperto = new EncriptadorExperto();

            var parametros = new Dictionary<string, object>
            {
                {"@Username", $"%{pUsuario.UserName}%"}
            };
            Usuario usuario = (Usuario)_connection.EjecutarEscalar(query, parametros);
            return usuario;
        }
        public List<Usuario> ObtenerUsuarios()
        {
            _connection.NuevaConexion(connectionString);
            string query = "SELECT Id, Nombre, Apellido, Email, Contrasena, Username, Bloqueado, CantidadIntentos FROM Usuario";
            var usuarios = _connection.EjecutarDataTable<Usuario>(query, new Dictionary<string, object>());
            _connection.FinalizarConexion();
            return usuarios;
        }
        public void AumentarIntentosFallidos(Usuario usuario)
        {
            _connection.NuevaConexion(connectionString);
            string query = "SELECT CantidadIntentos FROM Usuario WHERE Username = @Username";
            var parametros = new Dictionary<string, object>
            {
                {"@Username", usuario.UserName}
            };
            int cantIntentos = (int)_connection.EjecutarEscalar(query, parametros);

            if (cantIntentos >= 3)
            {
                query = "UPDATE Usuario SET Bloqueado = 1 WHERE Username = @Username";
                _connection.EjecutarSinResultado(query, parametros);
            }
            else
            {
                query = "UPDATE Usuario SET CantidadIntentos = CantidadIntentos + 1 WHERE Username = @Username";
                _connection.EjecutarSinResultado(query, parametros);
            }
            _connection.FinalizarConexion();
        }
            
        public List<Usuario> ObtenerUsuariosFiltrados(string filtro)
        {
            _connection.NuevaConexion(connectionString);
            string query = "SELECT Id, Nombre, Apellido, Email, Contrasena, UserName FROM Usuario WHERE Nombre LIKE @Filtro OR Apellido LIKE @Filtro OR Email LIKE @Filtro OR UserName LIKE @Filtro";
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
