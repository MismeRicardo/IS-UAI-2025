using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using IngSoft.DBConnection;
using IngSoft.DBConnection.Factory;
using IngSoft.DBConnection.Models;
using IngSoft.Domain;
using IngSoft.Domain.Enums;

namespace IngSoft.Repository
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly IConnection _connection;
        internal BitacoraRepository(IConnection connection)
        {
            _connection = connection ?? ConnectionFactory.CreateSqlServerConnection();
        }
        public void GuardarBitacora(Bitacora bitacora)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;
            _connection.NuevaConexion(connectionString);

            try
            {
                _connection.IniciarTransaccion();

                var parametros = new Dictionary<string, object>
                {
                    {"@Id", bitacora.Id},
                    {"@IdUsuario", bitacora.Usuario.IdUsuario },
                    {"@Fecha", bitacora.Fecha },
                    {"@Descripcion", bitacora.Descripcion },
                    {"@Origen", bitacora.Origen },
                    {"@TipoEvento", bitacora.TipoEvento }
                };

                _connection.EjecutarSinResultado("INSERT INTO Bitacora (Id, IdUsuario, Fecha, Descripcion, Origen, TipoEvento) VALUES (@Id, @IdUsuario, @Fecha, @Descripcion, @Origen, @TipoEvento)", parametros);

            }
            catch (Exception)
            {
                _connection.CancelarTransaccion();
                throw;
            }

            _connection.AceptarTransaccion();
            _connection.FinalizarConexion();
        }
        public List<Bitacora> ObtenerBitacoras()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;

                _connection.NuevaConexion(connectionString);

                var query = @"SELECT b.Id, b.Descripcion, b.Fecha, b.Origen, b.TipoEvento, u.Id AS IdUsuario, u.Nombre, u.Apellido, u.Email, u.Contrasena, u.UserName 
                              FROM Bitacora b
                              LEFT JOIN Usuario u ON b.IdUsuario = u.Id";

                var resultado = _connection.EjecutarDataTable<BitacoraQuerySql>(query, new Dictionary<string, object>());

                var bitacoras = resultado.Select( b => new Bitacora
                {
                    Id = b.Id,
                    Descripcion = b.Descripcion,
                    Fecha = b.Fecha,
                    Origen = b.Origen,
                    TipoEvento = (TipoEvento)b.TipoEvento,
                    Usuario = new Usuario
                    {
                        IdBitacora = b.UsuarioId,
                        Nombre = b.Nombre,
                        Apellido = b.Apellido,
                        Email = b.Email,
                        Contrasena = b.Contrasena,
                        UserName = b.UserName
                    }
                }).ToList();

                _connection.FinalizarConexion();

                return bitacoras;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Bitacora> ObtenerBitacorasFiltradas(string filtro)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["IngSoftConnection"].ConnectionString;

                _connection.NuevaConexion(connectionString);

                var parametros = new Dictionary<string, object>
                {
                    { "@Filtro", filtro }
                };

                var query = @"SELECT b.Id, b.Descripcion, b.Fecha, b.Origen, b.TipoEvento, u.Id AS IdUsuario, u.Nombre, u.Apellido, u.Email, u.Contrasena, u.UserName 
                              FROM Bitacora b
                              JOIN Usuario u ON b.IdUsuario = u.Id
                              WHERE b.Descripcion LIKE '%' + @Filtro + '%' 
                                 OR b.Origen LIKE '%' + @Filtro + '%' 
                                 OR u.UserName LIKE '%' + @Filtro + '%'";

                var resultado = _connection.EjecutarDataTable<BitacoraQuerySql>(query, parametros);

                var bitacoras = resultado.Select(b => new Bitacora
                {
                    Id = b.Id,
                    Descripcion = b.Descripcion,
                    Fecha = b.Fecha,
                    Origen = b.Origen,
                    TipoEvento = (TipoEvento)b.TipoEvento,
                    Usuario = new Usuario
                    {
                        IdBitacora = b.UsuarioId,
                        Nombre = b.Nombre,
                        Apellido = b.Apellido,
                        Email = b.Email,
                        Contrasena = b.Contrasena,
                        UserName = b.UserName
                    }
                }).ToList();

                _connection.FinalizarConexion();

                return bitacoras;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
