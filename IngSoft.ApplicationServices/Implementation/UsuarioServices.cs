using IngSoft.Abstractions;
using IngSoft.Domain;
using IngSoft.Repository;
using IngSoft.Services;
using IngSoft.Repository.Factory;
using IngSoft.ApplicationServices.Factory;
using System.Collections.Generic;
using System;
using IngSoft.Domain.Enums;

namespace IngSoft.ApplicationServices
{
    public class UsuarioServices: IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? FactoryRepository.CreateUsuarioRepository();
        }

        public void GuardarUsuario(Usuario usuario)
        {
            try
            {
               _usuarioRepository.GuardarUsuario(usuario);
                LogOnBitacora(new Usuario { IdUsuario = SessionManager.GetUsuario().IdUsuario }, "Usuario creado/modificado exitosamente", "GuardarUsuario", TipoEvento.Message);
            }
            catch(Exception)
            {
                LogOnBitacora(new Usuario { IdUsuario = SessionManager.GetUsuario().IdUsuario }, "Error al crear/modificar usuario", "GuardarUsuario", TipoEvento.Error);
                throw;
            }
        }
        public SessionManager LoginUser(Usuario usuario)
        {
            SessionManager session = SessionManager.GetInstance();
            Usuario usuarioStored = ObtenerUsuario(usuario);
            if(usuarioStored!= null && usuarioStored.Bloqueado)
            {
                LogOnBitacora(usuarioStored, "Intento de acceso con usuario bloqueado", "Login", TipoEvento.Error);
                throw new UnauthorizedAccessException("El usuario se encuentra bloqueado.");
            }
            try
            {
                session.LogIn(usuario, usuarioStored);
                LogOnBitacora(usuarioStored, "Acceso exitoso", "Login", TipoEvento.Message);
                _usuarioRepository.ResetearIntentosFallidos(usuario);
            }
            catch(UnauthorizedAccessException)
            {
                LogOnBitacora(usuarioStored, "Intento de acceso fallido", "Login", TipoEvento.Error);
                _usuarioRepository.AumentarIntentosFallidos(usuario);
                throw;
            }
            catch(Exception)
            {
                throw;
            }
            return session;
        }
        public void LogOutUser()
        {
            SessionManager.GetInstance().LogOut();
        }
        public Usuario ObtenerUsuario(Usuario usuario)
        {
            return _usuarioRepository.ObtenerUsuario(usuario);
        }

        public List<Usuario> ObtenerUsuarioFiltrados(string filtro)
        {
            return _usuarioRepository.ObtenerUsuariosFiltrados(filtro);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return _usuarioRepository.ObtenerUsuarios();
        }

        public void LogOnBitacora(Usuario usuario, string descripcion, string origen,TipoEvento tipoEvento)
        {
            IBitacoraServices _bitacoraServices;
            _bitacoraServices = ServicesFactory.CreateBitacoraServices();
            var bitacora = new Bitacora
            {
                Id = Guid.NewGuid(),
                Usuario = usuario,
                Fecha = DateTime.Now,
                Descripcion = descripcion,
                Origen = origen,
                TipoEvento = tipoEvento
            };
            _bitacoraServices.GuardarBitacora(bitacora);
        }
    }
}

