using IngSoft.ApplicationServices.Implementation;
using IngSoft.Repository.Factory;

namespace IngSoft.ApplicationServices.Factory
{
    public abstract class ServicesFactory
    {
        public static IBitacoraServices CreateBitacoraServices()
        {
            var bitacoraRepository = FactoryRepository.CreateBitacoraRepository();
            return new BitacoraServices(bitacoraRepository);
        }
        public static IMultiIdiomaServices CreateMultiIdiomaServices()
        {
            var idiomaRepository = FactoryRepository.CreateIdiomaRepository();
            var controlIdiomaRepository = FactoryRepository.CreateControlIdiomaRepository();
            return new MultiIdiomaServices(idiomaRepository, controlIdiomaRepository);
        }
    }
}
