namespace IngSoft.Abstractions
{
    public interface IIdioma : IEntity, ISubject
    {
        string Nombre { get; set; }
        string Codigo { get; set; }
        bool isDefault { get; set; }
    }
}
