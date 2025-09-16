namespace IngSoft.Abstractions
{
    public interface ISubject
    {
        void Suscribir(IObserver observer);
        void Desuscribir(IObserver observer);
        void NotificarObservers();
    }
}
