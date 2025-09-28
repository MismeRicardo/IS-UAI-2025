using System;
using System.Collections.Generic;
using System.Linq;
using IngSoft.Domain;
using IngSoft.DBConnection;
using IngSoft.DBConnection.Factory;
using IngSoft.Services.Encriptadores;
using IngSoft.Services;


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
               usuario.IdUsuario = _connection.ObtenerUltimoId("Usuario", "Id")+1;
                // Verificar si el nombre de usuario ya existe en la base de datos
                var existeUsuario = _connection.EjecutarEscalar("SELECT COUNT(*) FROM Usuario WHERE UserName = @UserName", new Dictionary<string, object>
                {
                    {"@UserName", usuario.UserName }
                });
                if (Convert.ToInt32(existeUsuario) > 0)
                {
                    throw new Exception("El nombre de usuario ya existe. Por favor, elija otro.");
                }


                // Preparar los parámetros para la consulta SQL
                var parametros = new Dictionary<string, object>
                {
                    {"@Id", usuario.IdUsuario},
                    {"@Nombre", usuario.Nombre },
                    {"@Apellido", usuario.Apellido },
                    {"@Email", usuario.Email },
                    {"@Contrasena", usuario.Contrasena },
                    {"@UserName", usuario.UserName },
                    {"@Bloqueado", 0},
                    {"@CantidadIntentos", 0}

                };
                _connection.EjecutarSinResultado("INSERT INTO Usuario (Id, Nombre, Apellido, Email, Contrasena, UserName, Bloqueado, CantidadIntentos) VALUES (@Id, @Nombre, @Apellido, @Email, @Contrasena, @UserName, @Bloqueado, @CantidadIntentos)", parametros);
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

            var parametros = new Dictionary<string, object>
            {
                {"@Username", pUsuario.UserName}
            };
            var resultado = _connection.EjecutarDataTable<Usuario> (query, parametros);
            Usuario usuario = null;
            if (resultado != null)
            {
                usuario = resultado.First<Usuario>();
            }
            return usuario;
        }

        public List<Usuario> ObtenerUsuarios()
        {
            _connection.NuevaConexion(connectionString);
            string query = "SELECT Id, Nombre, Apellido, Email, Contrasena, Username, Bloqueado, CantidadIntentos FROM Usuario";
            var resultado = _connection.EjecutarDataTable<Usuario>(query, new Dictionary<string, object>());
            List<Usuario> usuarios = resultado.Select(u => new Usuario
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Contrasena = u.Contrasena,
                UserName = u.UserName,
                Bloqueado = u.Bloqueado,
                CantidadIntentos = u.CantidadIntentos
            }).ToList();
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
