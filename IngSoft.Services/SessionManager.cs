using IngSoft.Abstractions;
using IngSoft.Services.Encriptadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngSoft.Services
{
    public class SessionManager
    {
        private static SessionManager uniqueInstance;
        private static IUsuario usuario;
        private SessionManager() { }

        public static SessionManager GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new SessionManager();
            }

            return uniqueInstance;
        }

        public static IUsuario GetUsuario() { return usuario; }

        public bool IsLoggedIn() { return usuario != null; }

        public SessionManager LogIn(IUsuario usuarioIngresado, IUsuario usuarioStored)
        {
            if(this.IsLoggedIn())
            {
                throw new InvalidOperationException("Ya hay un usuario logueado.");
            }

            if (usuarioStored is null || usuarioIngresado is null)
            {
                throw new ArgumentNullException("Credenciales inválidas.");
            }
            if(!VerificarPassword(usuarioIngresado, usuarioStored))
            {
                throw new UnauthorizedAccessException("Credenciales inválidas.");
            }
            usuarioIngresado.Contrasena = "";
            SessionManager.usuario = usuarioIngresado;
            return this;
        }

        public bool VerificarPassword(IUsuario pUsuario, IUsuario pUsuarioStored)
        {
            return new EncriptadorExperto().DesencriptadorSecuencial(pUsuarioStored.Contrasena, pUsuario.Contrasena);
        }
        internal bool VerificarPasswordHash(IEncriptadorHash encriptador,IUsuario pUsuario,IUsuario pUsuarioStored)
        {
            return encriptador.VerificarHash(pUsuario.Contrasena, pUsuarioStored.Contrasena);
        }



        public void LogOut()
        {
            uniqueInstance = null;
            usuario = null;
        }
        //internal static void SetUsuario(IUsuario usuario)
        //{
        //    if (uniqueInstance == null)
        //    {
        //    }
        //}
    }
}
