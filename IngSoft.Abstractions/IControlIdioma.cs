using System;

namespace IngSoft.Abstractions
{
    public interface IControlIdioma: IEntity
    {
        string NombreControl { get; set; }
        Guid IdIdioma { get; set; }
        string TextoTraducido { get; set; }
    }
}
