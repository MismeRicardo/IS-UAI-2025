using System;
using System.Collections.Generic;
using IngSoft.Domain;
using IngSoft.Repository;
using IngSoft.Repository.Factory;

namespace IngSoft.ApplicationServices.Implementation
{
    public class MultiIdiomaServices : IMultiIdiomaServices
    {
        private readonly IIdiomaRepository _idiomaRepository;
        private readonly IControlIdiomaRepository _controlIdiomaRepository;
        public MultiIdiomaServices(IIdiomaRepository idiomaRepository, IControlIdiomaRepository controlIdiomaRepository)
        {
            _idiomaRepository = idiomaRepository ?? FactoryRepository.CreateIdiomaRepository();
            _controlIdiomaRepository = controlIdiomaRepository ?? FactoryRepository.CreateControlIdiomaRepository();
        }

        public List<ControlIdioma> ObtenerControlesPorIdioma(Guid idiomaId)
        {
            return _controlIdiomaRepository.ObtenerControlesPorIdioma(idiomaId);
        }

        public Idioma ObtenerIdiomaPorDefecto()
        {
            return _idiomaRepository.ObtenerIdiomaPorDefecto();
        }

        public List<Idioma> ObtenerIdiomas()
        {
            return _idiomaRepository.ObtenerIdiomas();
        }
    }
}
