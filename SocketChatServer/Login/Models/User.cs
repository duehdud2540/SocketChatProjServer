namespace SocketChatServer.Login.Models
{
    //계정 상태 enum
    public enum state : byte
    {
        None = 0,             // 기본값 / 정의되지 않음
        Active = 1,           // 정상 활성 상태 (로그인 및 서비스 이용 가능)
        PendingApproval = 2,  // 승인 대기 / 이메일 인증 대기 상태
        Suspended = 3,        // 일시 정지 (제재 또는 비밀번호 5회 오류 등)
        Banned = 4,           // 영구 정지 (관리자에 의한 영구 차단)
        Dormant = 5,          // 휴면 계정 (장기 미접속)
        Deactivated = 6       // 탈퇴 완료 (유예 기간 또는 삭제 대기)
    }

    //친구 관계 상태
    public enum relationState : byte
    {
        None = 0,
        Pending = 1,    // 친구 요청 전송 (수락 대기 중)
        Accepted = 2,   // 친구 상태 (수락 완료)
        Declined = 3,   // 요청 거절
        Blocked = 4     // 차단 (해당 유저 메시지/요청 차단)
    }
    public class User
    {
        public string UserGuid { get; init; } = $"usr_{Guid.NewGuid():N}"; // 유저 uuid
        public string id { get; set; } = null!; // 유저가 로그인 시 사용할 id
        public string password {  get; set; } = null!; // 비밀번호
        public string name { get; set; } = null!; // 유저의 실명
        public string nickName { get; set; } = null!; // 채팅방에서 사용할 닉네임
        public state state { get; set; } // 유저 계정 상태
        public DateOnly birtDate { get; set; } // 생년월일
        public DateTime createAt { get; init; } = DateTime.Now; // 생성일자
    }

    public class Relation
    {
        public string RelationGuid { get; init; } = $"rel_{Guid.NewGuid():N}";// 관계 아이디 
        public string RequestId { get; set; } = null!; // 요청자
        public string addressId { get; set; } = null!;// 응답자
        public relationState rState { get; set; } // 관계
        public DateTime createAt { get; init; } = DateTime.Now; // 최초 요청일
        public DateTime updateAt { get; set; } = DateTime.Now; // 업데이트일자

    }

    public class CreateUserRequest
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateOnly birthDate { get; set; }
        public string password { get; set; }
        public string nickName { get; set; }
    }
}
