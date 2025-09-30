using IngSoft.DBConnection;
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
        public static IUsuarioRepository CreateUsuarioRepository()
        {
            IConnection connection = ConnectionFactory.CreateSqlServerConnection();
            return new UsuarioRepository(connection);
        }
    }
}
