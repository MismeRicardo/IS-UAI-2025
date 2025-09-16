using System;
using System.Collections.Generic;
using IngSoft.Domain;

namespace IngSoft.Repository
{
    public interface IControlIdiomaRepository
    {
        List<ControlIdioma> ObtenerControlesPorIdioma(Guid idiomaId);
    }
}
