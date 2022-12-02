using CityForum.Services.Models;

namespace CityForum.Services.Abstract;

public interface IUserService
{
    UserModel GetUser(Guid id);
    void DeleteUser(Guid id);
    public PageModel<UserModel> GetUsers(int limit = 20, int offset = 0);
}