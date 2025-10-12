﻿using IngSoft.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace IngSoft.Domain
{
    public abstract class PermisoComponent : Entity, ICompositable
    {
        private ICompositable parent;
        public string Nombre { get; set; }

        // Delegate y evento que notifica que este permiso fue eliminado de un agrupamiento
        public delegate void PermisoEliminadoHandler(PermisoComponent permiso);
        public event PermisoEliminadoHandler PermisoEliminado;

        // Delegate y evento que notifica que este permiso fue asignado a un padre
        public delegate void PermisoAsignadoHandler(PermisoComponent padre);
        public event PermisoAsignadoHandler PermisoAsignado;

        // Constructor protegido para suscribirse al manejador por defecto
        protected PermisoComponent()
        {
            // Suscribir handler por defecto que limpia el padre cuando este componente es eliminado
            PermisoEliminado += DefaultOnPermisoEliminado;
            // Suscribir handler por defecto que asigna el padre cuando este componente recibe la notificación
            PermisoAsignado += DefaultOnPermisoAsignado;
        }

        // Hacer la propiedad accesible dentro del ensamblado además de derivadas para permitir asignarla a otros objetos
        public ICompositable Parent {
            get
            {
                return parent;
            }
            private set
            {
                if (value != null && value.Nombre == this.Nombre)
                {
                    throw new InvalidOperationException("Un componente no puede ser su propio padre.");
                }
                if(HasParent() && value != null)
                {
                    throw new InvalidOperationException("El componente ya tiene un padre asignado.");
                }
                parent = value;
            }
        }

        // Handler por defecto: cuando se notifica que este permiso fue eliminado, limpiar su Parent
        private void DefaultOnPermisoEliminado(PermisoComponent permiso)
        {
            if (ReferenceEquals(this, permiso))
            {
                // Al limpiar Parent usamos la propiedad que ahora soporta null sin lanzar NRE
                this.Parent = null;
            }
        }

        // Handler por defecto: cuando se notifica que este permiso fue asignado a un padre, setear Parent
        private void DefaultOnPermisoAsignado(PermisoComponent padre)
        {
            if (padre != null)
            {
                // El setter validará condiciones (no permitir self-asignación o re-asignación)
                this.Parent = padre;
            }
        }

        // Método público para que quien remueva pueda invocar la notificación sobre el componente
        public void RaisePermisoEliminado()
        {
            PermisoEliminado?.Invoke(this);
        }

        // Método público para que quien agregue pueda invocar la notificación de asignación
        public void RaisePermisoAsignado(ICompositable padre)
        {
            // Disparar el evento con PermisoComponent si el padre es PermisoComponent
            PermisoAsignado?.Invoke(padre as PermisoComponent);
        }

        public virtual bool Operacion(string userAction)
        {
            return Nombre==userAction;
        }
        public virtual bool HasParent()
        {
            return Parent != null;
        }
        public virtual ICompositable AddCompositable(ICompositable compositable)
        {
            if(HasParent())
            {
                if(compositable != null)
                {
                    Parent.AddCompositable(compositable);
                    return Parent;
                }
                else
                {
                    throw new ArgumentNullException("El permiso a agregar no puede ser nulo.");
                }
            }
            else
            {
                return null;
            }
        }
        public virtual ICompositable RemoveCompositable(ICompositable compositable = null)
        {
            if(HasParent())
            {
                Parent.RemoveCompositable(this);
                return Parent;
            }
            else
            {
                return null;
            }
        }
        public virtual ICompositable ClearCompositable()
        {
            if(HasParent())
            {
                Parent.ClearCompositable();
                return Parent;
            }
            else
            {
                return null;
            }
        }

        public virtual ICollection<ICompositable> GetListCompositable()
        {
            ICollection<ICompositable> collection = new List<ICompositable>();
            collection.Add(this);
            return collection;
        }
    }
}
