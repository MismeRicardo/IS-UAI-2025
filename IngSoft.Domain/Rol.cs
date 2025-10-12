using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IngSoft.Abstractions;

namespace IngSoft.Domain
{
    internal class Rol : Entity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public IEnumerable<ICompositable> Permisos { get; set; }
    }
}
