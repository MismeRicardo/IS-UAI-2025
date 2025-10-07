using IngSoft.Abstractions;
using IngSoft.Domain;
using IngSoft.Repository;
using IngSoft.Services;
using IngSoft.Repository.Factory;
using System.Collections.Generic;
using System;
using IngSoft.Domain.Enums;

namespace IngSoft.ApplicationServices
{
    public class UsuarioServices: IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private static Action<Usuario, string, string, TipoEvento> _registrarEnBitacora;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? FactoryRepository.CreateUsuarioRepository();
        }

        public void SetRegistradoBitacora(Action<Usuario, string, string, TipoEvento> registrarEnBitacora)
        {
            _registrarEnBitacora = registrarEnBitacora; //?? LogOnBitacora;
        }

        public void GuardarUsuario(Usuario usuario)
        {
            try
            {
               _usuarioRepository.GuardarUsuario(usuario);
               _registrarEnBitacora(new Usuario { IdUsuario = SessionManager.GetUsuario().IdUsuario }, "Usuario creado/modificado exitosamente", "GuardarUsuario", TipoEvento.Message);
            }
            catch(Exception)
            {
                _registrarEnBitacora(new Usuario { IdUsuario = SessionManager.GetUsuario().IdUsuario }, "Error al crear/modificar usuario", "GuardarUsuario", TipoEvento.Error);
                throw;
            }
        }
        public SessionManager LoginUser(Usuario usuario)
        {
            SessionManager session = SessionManager.GetInstance();
            Usuario usuarioStored = ObtenerUsuario(usuario);
            if(usuarioStored!= null && usuarioStored.Bloqueado)
            {
                _registrarEnBitacora(usuarioStored, "Intento de acceso con usuario bloqueado", "Login", TipoEvento.Warning);
                throw new UnauthorizedAccessException("El usuario se encuentra bloqueado.");
            }
            try
            {
                session.LogIn(usuario, usuarioStored);
                _registrarEnBitacora(usuarioStored, "Acceso exitoso", "Login", TipoEvento.Message);
                _usuarioRepository.ResetearIntentosFallidos(usuario);
            }
            catch(UnauthorizedAccessException)
            {
                _registrarEnBitacora(usuarioStored, "Intento de acceso fallido", "Login", TipoEvento.Warning);
                _usuarioRepository.AumentarIntentosFallidos(usuario);
                throw;
            }
            catch(Exception e)
            {
                _registrarEnBitacora(usuarioStored, "Error inesperado: "+e.Message, "Login", TipoEvento.Error);
                throw;
            }
            return session;
        }
        public void LogOutUser()
        {
            _registrarEnBitacora(SessionManager.GetUsuario() as Usuario, "Cierre de sesión exitoso", "LogOut", TipoEvento.Message);
            SessionManager.GetInstance().LogOut();
        }
        public Usuario ObtenerUsuario(Usuario usuario)
        {
            try
            {
                Usuario mUsuario = _usuarioRepository.ObtenerUsuario(usuario);
                if (!(SessionManager.GetUsuario() is null))
                {
                    _registrarEnBitacora(SessionManager.GetUsuario() as Usuario, "Buscado Usuario "+usuario.Username, "ObtenerUsuario", TipoEvento.Message);
                }
                return mUsuario;
            }
            catch(Exception e)
            {
                _registrarEnBitacora(SessionManager.GetUsuario() as Usuario, "Error inesperado: " + e.Message, "Login", TipoEvento.Error);
                throw;
            }
        }

        public List<Usuario> ObtenerUsuarioFiltrados(string filtro)
        {
            try
            {
                List<Usuario> mUsuarios = _usuarioRepository.ObtenerUsuariosFiltrados(filtro);
                _registrarEnBitacora(SessionManager.GetUsuario() as Usuario, "Buscados todos los Usuarios con filtro " +filtro , "ObtenerUsuariosFiltrados", TipoEvento.Message);
                return mUsuarios;
            }
            catch (Exception e)
            {
                _registrarEnBitacora(SessionManager.GetUsuario() as Usuario, "Error inesperado: " + e.Message, "Login", TipoEvento.Error);
                throw;
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            try
            {
                List<Usuario> mUsuarios = _usuarioRepository.ObtenerUsuarios();
                _registrarEnBitacora(SessionManager.GetUsuario() as Usuario, "Buscados todos los Usuarios", "ObtenerUsuariosFiltrados", TipoEvento.Message);
                return mUsuarios;
            }
            catch (Exception e)
            {
                _registrarEnBitacora(SessionManager.GetUsuario() as Usuario, "Error inesperado: " + e.Message, "Login", TipoEvento.Error);
                throw;
            }
        }

        //private void LogOnBitacora(Usuario usuario, string descripcion, string origen,TipoEvento tipoEvento)
        //{
        //    IBitacoraServices _bitacoraServices;
        //    _bitacoraServices = ServicesFactory.CreateBitacoraServices();
        //    var bitacora = new Bitacora
        //    {
        //        Id = Guid.NewGuid(),
        //        Usuario = usuario,
        //        Fecha = DateTime.Now,
        //        Descripcion = descripcion,
        //        Origen = origen,
        //        TipoEvento = tipoEvento
        //    };
        //    _bitacoraServices.GuardarBitacora(bitacora);
        //}
    }
}

