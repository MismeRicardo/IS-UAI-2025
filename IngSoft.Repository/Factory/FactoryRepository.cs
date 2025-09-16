using IngSoft.DBConnection.Factory;
using IngSoft.Repository.Implementation;

namespace IngSoft.Repository.Factory
{
    public abstract class FactoryRepository
    {
        public static IBitacoraRepository CreateBitacoraRepository()
        {
            var connection = ConnectionFactory.CreateSqlServerConnection();
            return new BitacoraRepository(connection);
        }
        public static IIdiomaRepository CreateIdiomaRepository()
        {
            var connection = ConnectionFactory.CreateSqlServerConnection();
            return new IdiomaRepository(connection);
        }
        public static IControlIdiomaRepository CreateControlIdiomaRepository()
        {
            var connection = ConnectionFactory.CreateSqlServerConnection();
            return new ControlIdiomaRepository(connection);
        }
    }
}
