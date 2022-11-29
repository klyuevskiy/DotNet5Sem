using CityForum.Services.Models;
using CityForum.Repository;
using CityForum.Entities.Models;
using CityForum.Services.Abstract;
using AutoMapper;

namespace CityForum.Services.Impelementation;

public class UserService : IUserService
{
    private readonly IRepository<User> usersRepository;
    private readonly IMapper mapper;

    public UserService(IRepository<User> usersRepository, IMapper mapper)
    {
        this.usersRepository = usersRepository;
        this.mapper = mapper;
    }

    private User GetUserFromRepository(Guid id)
    {
        User? user = usersRepository.GetById(id);
        if (user == null)
        {
            throw new Exception($"User not found, id = {id}");
        }
        return user;
    }

    public void DeleteUser(Guid id)
    {
        usersRepository.Delete(GetUserFromRepository(id));
    }

    public UserModel GetUser(Guid id)
    {
        return mapper.Map<UserModel>(GetUserFromRepository(id));
    }
}