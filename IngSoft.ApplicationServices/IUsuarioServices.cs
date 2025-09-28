using IngSoft.Domain;
using IngSoft.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngSoft.ApplicationServices
{
    public interface IUsuarioServices
    {
        void GuardarUsuario(Usuario usuario);
        List<Usuario> ObtenerUsuarios();
        List<Usuario> ObtenerUsuarioFiltrados(string filtro);
        Usuario ObtenerUsuario(Usuario usuario);
        SessionManager LoginUser(Usuario usuario);
        void LogOutUser();
    }
}