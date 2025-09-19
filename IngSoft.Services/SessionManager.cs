using IngSoft.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngSoft.Services
{
    internal class SessionManager
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

            SessionManager.usuario = usuarioIngresado;
            return this;
        }

        public bool VerificarPasswordHash(IEncriptadorHash encriptador,IUsuario mUsuario,IUsuario mUsuarioStored)
        {
            return encriptador.VerificarHash(mUsuario.Contrasena, mUsuarioStored.Contrasena);
        }



        public void LogOut()
        {
            uniqueInstance = null;
            usuario = null;
        }
        public static void SetUsuario(IUsuario usuario)
        {
            if (uniqueInstance == null)
            {
            }
        }
    }
}
