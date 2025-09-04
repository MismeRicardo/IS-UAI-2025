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
    }
}
