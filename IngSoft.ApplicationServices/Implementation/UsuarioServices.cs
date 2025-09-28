using IngSoft.Abstractions;
using IngSoft.Domain;
using IngSoft.Repository;
using IngSoft.Repository.Factory;
using IngSoft.Services;
using System.Collections.Generic;
using System;

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
            }
            catch(Exception)
            {
                
                throw;
            }
        }
        public SessionManager LoginUser(Usuario usuario)
        {
            SessionManager session = SessionManager.GetInstance();
            Usuario usuarioStored = ObtenerUsuario(usuario);
            try
            {
                session.LogIn(usuario, usuarioStored);
            }
            catch(UnauthorizedAccessException)
            {
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
    }
}
