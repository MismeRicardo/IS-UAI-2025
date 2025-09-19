using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngSoft.DBConnection.Models
{
    public class UsuarioQuerySql
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public string UserName { get; set; }
        public bool Bloqueado { get; set; }
        public int IntentosFallidos { get; set; }


    }
}
