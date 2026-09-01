using Dapper;
using SocketChatServer.Data;
using SocketChatServer.Login.Models;

namespace SocketChatServer.Login.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DapperDBContext _dbContext;
        // 1. 생성자를 통해 DapperDBContext 주입
        public UserRepo(DapperDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // [TODO 1] 유저 ID로 단건 조회
        public async Task<User?> GetUserByIdAsync(string userId)
        {
            using var connection = _dbContext.CreateConnection();
            const string sql = @"
                    SELECT USER_GUID as UserGuid,
                           ID as id,
                           PASSWORD as password,
                           NAME as name,
                           NICK_NAME as nickName,
                           STATE as state,
                           BIRTH_DATE as birtDate,
                           CREATE_AT as createAt
                      FROM USERS
                     WHERE ID = :UserId";
            // TODO: Dapper의 QuerySingleOrDefaultAsync<User>()를 호출하여 결과를 반환해보세요.
            // 힌트: await connection.QuerySingleOrDefaultAsync<User>(sql, new { UserId = userId });
            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { UserId = userId });
        }
        // [TODO 2] 신규 회원 등록
        public async Task<bool> CreateUserAsync(User user)
        {
            using var connection = _dbContext.CreateConnection();

            const string sql = @"
                    INSERT INTO USERS (user_uuid, ID, PASSWORD, NAME, NICK_NAME, STATE, BIRTH_DATE, CREATE_AT)
                    VALUES (:UserGuid, :Id, :password, :Name, :nickName, :State, :birtDate, :createAt)";

            // TODO: ExecuteAsync()를 실행하고 성공 row 수가 1 이상인지 확인해보세요.
            return await connection.ExecuteAsync(sql) == 1;
        }

        // [TODO 3] ID 중복 여부 확인
        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            using var connection = _dbContext.CreateConnection();

            const string sql = "SELECT COUNT(1) FROM USERS WHERE ID = :UserId";
            return await connection.ExecuteAsync(sql)==0;
        }
    }
}
