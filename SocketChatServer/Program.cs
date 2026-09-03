
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Diagnostics;

namespace SocketChatServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();
            

            //로그 설정
            var logger= app.Services.GetRequiredService<ILogger<Program>>();
            var configuration = app.Services.GetRequiredService<IConfiguration>();
            string? connectionString = configuration.GetConnectionString("defaultConnection");
            logger.LogInformation("============================================================");
            logger.LogInformation("======================DB연결 시도중...======================");
            if (string.IsNullOrEmpty(connectionString))
            {
                logger.LogError("DB 연결 에러");
            }else
            {
                var stopWatch = Stopwatch.StartNew();
                try
                {
                    using IDbConnection connection = new OracleConnection(connectionString);

                    connection.Open();
                    stopWatch.Stop();
                    logger.LogInformation(">>> [DB Test] DB 연결 성공! (상태: {State}, 소요시간: {Elapsed}ms)", connection.State, stopWatch.ElapsedMilliseconds);
                    string testQuery = "SELECT 1";
                    int result = connection.ExecuteScalar<int>(testQuery);

                    logger.LogInformation(">>> [DB Test] Dapper 쿼리 응답 성공! (테스트 결과: {Result})", result);
                    logger.LogInformation("==================================================");
                
                }
                catch(Exception ex)
                {
                    stopWatch.Stop();
                    logger.LogError("==================================================");
                    logger.LogError(ex, ">>> [DB Test] DB 연결 실패! (소요시간: {Elapsed}ms, 사유: {Message})", stopWatch.ElapsedMilliseconds, ex.Message);
                    logger.LogError("==================================================");

                }
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGrpcService<SocketChatServer.Login.Controller.AuthServiceImpl>();
            app.MapControllers();

            app.Run();
        }
    }
}
