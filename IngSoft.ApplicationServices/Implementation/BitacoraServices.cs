using System.Collections.Generic;
using IngSoft.Domain;
using IngSoft.Repository;
using IngSoft.Repository.Factory;

namespace IngSoft.ApplicationServices.Implementation
{
    public class BitacoraServices: IBitacoraServices
    {
        private readonly IBitacoraRepository _bitacoraRepository;
        public BitacoraServices(IBitacoraRepository bitacoraRepository)
        {
            _bitacoraRepository = bitacoraRepository ?? FactoryRepository.CreateBitacoraRepository();
        }
        public void GuardarBitacora(Bitacora bitacora)
        {
            _bitacoraRepository.GuardarBitacora(bitacora);
        }
        public List<Bitacora> ObtenerBitacoras()
        {
            return _bitacoraRepository.ObtenerBitacoras();
        }

        public List<Bitacora> ObtenerBitacorasFiltradas(string filtro)
        {
            return _bitacoraRepository.ObtenerBitacorasFiltradas(filtro);
        }
    }
}
