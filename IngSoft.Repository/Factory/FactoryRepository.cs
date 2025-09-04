using IngSoft.DBConnection.Factory;

namespace IngSoft.Repository.Factory
{
    public abstract class FactoryRepository
    {
        public static IBitacoraRepository CreateBitacoraRepository()
        {
            var connection = ConnectionFactory.CreateSqlServerConnection();
            return new BitacoraRepository(connection);
        }
    }
}
