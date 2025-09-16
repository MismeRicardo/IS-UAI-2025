using System.Collections.Generic;
using IngSoft.Domain;

namespace IngSoft.Repository
{
    public interface IIdiomaRepository
    {
        List<Idioma> ObtenerIdiomas();
        Idioma ObtenerIdiomaPorDefecto();
        
        void CrearIdioma(Idioma idioma);
    }
}
