using System.Collections.Generic;
using IngSoft.Abstractions;

namespace IngSoft.Domain
{
    public class Idioma: Entity, IIdioma
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool isDefault { get; set; }

        private readonly List<IObserver> _observers = new List<IObserver>();

        public void Suscribir(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Desuscribir(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotificarObservers()
        {
            _observers.ForEach(o => o.Actualizar());
        }
    }
}
