using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace SocketChatServer.Data
{
    public class DapperDBContext
    {
        private readonly string _connectionString;
        public DapperDBContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("연결된 문자열이 없습니다.");
        }

        public IDbConnection CreateConnection() => new OracleConnection(_connectionString);
    }
}
