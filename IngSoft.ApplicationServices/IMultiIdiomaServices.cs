using System;
using System.Collections.Generic;
using IngSoft.Domain;

namespace IngSoft.ApplicationServices
{
    public interface IMultiIdiomaServices
    {
        List<Idioma> ObtenerIdiomas();
        List<ControlIdioma> ObtenerControlesPorIdioma(Guid idiomaId);
        Idioma ObtenerIdiomaPorDefecto();
    }
}
