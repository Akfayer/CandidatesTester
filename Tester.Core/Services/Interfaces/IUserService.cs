using Tester.Core.Models;

namespace Tester.Core.Services.Interfaces;

public interface IUserService
{
    Task RegisterAsync(UserModel userModel);
    Task<AuthModel?> AuthenticateAsync(LoginModel loginRequest);
    Task<List<UserModel>> GetAllUsersAsync();
}