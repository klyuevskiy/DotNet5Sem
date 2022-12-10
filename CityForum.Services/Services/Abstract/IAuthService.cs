using CityForum.Services.Models;

namespace CityForum.Services.Abstract;

public interface IAuthService
{
    Task<UserModel> RegisterUser(RegisterUserModel model);
    Task<IdentityModel.Client.TokenResponse> LoginUser(LoginUserModel model);
}