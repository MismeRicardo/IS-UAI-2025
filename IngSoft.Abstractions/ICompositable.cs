using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngSoft.Abstractions
{
    // Interfaz que contiene únicamente las propiedades/atributos
    public interface IComposible
    {
        string Nombre { get; set; }
        ICompositable Parent { get; } // Parent es de solo lectura en la interfaz para evitar asignación externa
    }

    // Interfaz que contiene únicamente la metodología/operaciones
    public interface ICompositableMethodology
    {
        bool Operacion(string userAction);
        ICompositable AddCompositable(ICompositable compositable);
        ICompositable RemoveCompositable(ICompositable compositable);
        ICompositable ClearCompositable();
        void RaisePermisoEliminado();
        void RaisePermisoAsignado(ICompositable padre);
    }

    // Interfaz compuesta para compatibilidad: agrupa propiedades y métodos
    public interface ICompositable : IComposible, ICompositableMethodology
    {
    }
}
