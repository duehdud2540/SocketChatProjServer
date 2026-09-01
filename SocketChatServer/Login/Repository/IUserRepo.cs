namespace SocketChatServer.Login.Repository;

using Microsoft.AspNetCore.Identity.Data;
using SocketChatServer.Login.Models;
public interface IUserRepo
{
    Task<User?> GetUserRepoAsync(string userId);
    Task<bool> CreateUserAsync(CreateUserRequest user);  // 회원가입시 
}
