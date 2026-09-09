
using Grpc.Core;
using SocketChatServer.Protos;

namespace SocketChatServer.Login.Controller
{
    public class AuthServiceImpl : AuthService.AuthServiceBase
    {
        private readonly ILogger<AuthServiceImpl> _logger;
        public AuthServiceImpl(ILogger<AuthServiceImpl> logger)
        {
            _logger = logger;
        }

        // 클라이언트의 LoginAsync 호출을 받아 응답하는 메서드
        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            _logger.LogInformation(">>> [gRPC Login] 요청 수신 - UserId: {UserId}", request.UserId);

            // 임시 응답 (DB 연동 전 테스트용)
            var response = new LoginResponse
            {
                IsSuccess = true,
                Message = "로그인에 성공하였습니다.",
                Nickname = request.UserId,
                Token = "test-token-12345"
            };

            return Task.FromResult(response);

            // 회원가입




            //로그인




            //비밀번호 변경




            //아이디/ 패스워드 찾기
        }
    }
}
