using System;
using System.Collections.Generic;
using System.Linq;
using IngSoft.Domain;
using IngSoft.DBConnection;
using IngSoft.DBConnection.Factory;
using IngSoft.Services.Encriptadores;
using IngSoft.Services;
using IngSoft.DBConnection.Models;

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
                    {"@UserName", usuario.Username }
                });
                // Preparar los parámetros para la consulta SQL
                var parametros = new Dictionary<string, object>
                {
                    {"@Id", usuario.IdUsuario},
                    {"@Nombre", usuario.Nombre },
                    {"@Apellido", usuario.Apellido },
                    {"@Email", usuario.Email },
                    {"@Contrasena", usuario.Contrasena },
                    {"@UserName", usuario.Username },
                    {"@Bloqueado", 0},
                    {"@CantidadIntentos", 0}
                };
                if (Convert.ToInt32(existeUsuario) > 0)
                {
                    _connection.EjecutarSinResultado("UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Contrasena = @Contrasena, UserName = @UserName, Bloqueado = @Bloqueado, CantidadIntentos = @CantidadIntentos WHERE Username = @UserName", parametros);
                }
                else
                {
                    _connection.EjecutarSinResultado("INSERT INTO Usuario (Id, Nombre, Apellido, Email, Contrasena, UserName, Bloqueado, CantidadIntentos) VALUES (@Id, @Nombre, @Apellido, @Email, @Contrasena, @UserName, @Bloqueado, @CantidadIntentos)", parametros);
                }
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
                {"@Username", pUsuario.Username}
            };
            var resultado = _connection.EjecutarDataTable<UsuarioQuerySql> (query, parametros);
            Usuario usuario = null;
            if (resultado != null)
            {
                if(resultado.Count > 0)
                {
                    usuario = resultado.Select(u => new Usuario
                    {
                        IdUsuario = u.IdUsuario,
                        Id = EncriptarId(u.Id),
                        Nombre = u.Nombre,
                        Apellido = u.Apellido,
                        Email = u.Email,
                        Contrasena = u.Contrasena,
                        Username = u.Username,
                        Bloqueado = u.Bloqueado,
                        CantidadIntentos = u.CantidadIntentos
                    }).First<Usuario>();
                }
                else
                {
                    throw new UnauthorizedAccessException("Usuario no encontrado.");
                }
            }
            return usuario;
        }

        public List<Usuario> ObtenerUsuarios()
        {
            _connection.NuevaConexion(connectionString);
            string query = "SELECT Id, Nombre, Apellido, Email, Contrasena, Username, Bloqueado, CantidadIntentos FROM Usuario";
            var resultado = _connection.EjecutarDataTable<UsuarioQuerySql>(query, new Dictionary<string, object>());
            List<Usuario> usuarios = resultado.Select(u => new Usuario
            {
                Id = EncriptarId(u.Id),
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Contrasena = u.Contrasena,
                Username = u.Username,
                Bloqueado = u.Bloqueado,
                CantidadIntentos = u.CantidadIntentos
            }).ToList();
            _connection.FinalizarConexion();
            return usuarios;
        }
        internal Guid EncriptarId(Guid id)
        {
            return new Guid(new EncriptadorExperto().EncriptadorOnlyHash(id.ToString()));
        }
        public void AumentarIntentosFallidos(Usuario usuario)
        {
            _connection.NuevaConexion(connectionString);
            string query = "SELECT CantidadIntentos FROM Usuario WHERE Username = @Username";
            var parametros = new Dictionary<string, object>
            {
                {"@Username", usuario.Username}
            };
            int cantIntentos = (int)_connection.EjecutarEscalar(query, parametros);

            if (cantIntentos >= 2)
            {
                query = "UPDATE Usuario SET Bloqueado = 1 WHERE Username = @Username";
                _connection.EjecutarSinResultado(query, parametros);
            }
            else
            {
                query = "UPDATE Usuario SET Bloqueado = 1 WHERE Username = @Username";
                _connection.EjecutarSinResultado(query, parametros);
                query = "UPDATE Usuario SET CantidadIntentos = CantidadIntentos + 1 WHERE Username = @Username";
                _connection.EjecutarSinResultado(query, parametros);
            }
            _connection.FinalizarConexion();
        }

        public void ResetearIntentosFallidos(Usuario usuario)
        {
            _connection.NuevaConexion(connectionString);
            string query = "UPDATE Usuario SET CantidadIntentos = 0 WHERE Username = @Username";
            var parametros = new Dictionary<string, object>
            {
                {"@Username", usuario.Username}
            };
            _connection.EjecutarSinResultado(query, parametros);
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
            var resultado = _connection.EjecutarDataTable<UsuarioQuerySql>(query, parametros);
             List<Usuario> usuarios = resultado.Select(u => new Usuario
            {
                Id = EncriptarId(u.Id),
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Contrasena = u.Contrasena,
                Username = u.Username,
                Bloqueado = u.Bloqueado,
                CantidadIntentos = u.CantidadIntentos
            }).ToList();
            _connection.FinalizarConexion();
            return usuarios;
        }
    }
}
