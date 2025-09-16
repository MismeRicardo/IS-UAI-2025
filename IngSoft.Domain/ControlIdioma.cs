using System;
using IngSoft.Abstractions;

namespace IngSoft.Domain
{
    public class ControlIdioma: Entity , IControlIdioma
    {
        public string NombreControl { get; set; }
        public Guid IdIdioma { get; set; }
        public string TextoTraducido { get; set; }
    }
}
