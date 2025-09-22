using System;
using System.Collections.Generic;

namespace IngSoft.Abstractions
{
    public interface IUsuario
    {
        Guid IdBitacora { get; set; }
        int Id { get; set; }
        string UserName {  get; set; }
        string Contrasena {  get; set; }
    }

    public interface ICompositable
    {
        string Nombre {  get; set; }
        object Operacion();

        ICollection<ICompositable> GetListCompositable();

        void AddCompositable(ICompositable compositable);
        void RemoveCompositable(ICompositable compositable);
        void ClearCompositable();

    }
}
