using AutoMapper;
using CityForum.Entities.Models;
using CityForum.Repository;
using CityForum.Services.Abstract;
using CityForum.Services.Models;
using CityForum.Shared.Exceptions;
using CityForum.Shared.ResultCodes;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CityForum.Services.Impelementation;

public class AuthService : IAuthService
{
    private readonly IRepository<User> usersRepository;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly string identityUri;

    public AuthService(
        IRepository<User> usersRepository,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper,
        IConfiguration configuration)
    {
        this.usersRepository = usersRepository;
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.mapper = mapper;
        this.configuration = configuration;
        identityUri = configuration.GetValue<string>("IdentityServer:Uri");
    }

    public async Task<UserModel> RegisterUser(RegisterUserModel model)
    {
        var existingUser = usersRepository.GetAll()
        .Where(x => x.Login.ToLower() == model.Login.ToLower())
        .FirstOrDefault();

        if (existingUser != null)
        {
            throw new LogicException(ResultCode.USER_ALREADY_EXISTS);
        }

        var user = new User()
        {
            Login = model.Login,
            UserName = model.Login
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            throw new LogicException(ResultCode.IDENTITY_SERVER_ERROR);
        }

        var createdUser = usersRepository.GetAll()
        .Where(x => x.Login.ToLower() == model.Login.ToLower())
        .FirstOrDefault();

        return mapper.Map<UserModel>(createdUser);
    }

    public async Task<IdentityModel.Client.TokenResponse> LoginUser(LoginUserModel model)
    {
        var user = usersRepository.GetAll()
        .Where(x => x.Login.ToLower() == model.Login.ToLower())
        .FirstOrDefault();
        if (user == null)
        {
            throw new LogicException(ResultCode.LOGIN_OR_PASSWORD_IS_INCORRECT);
        }

        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync(identityUri);
        if (disco.IsError)
        {
            throw new Exception(disco.Error);
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
        {
            Address = disco.TokenEndpoint,
            ClientId = model.ClientId,
            ClientSecret = model.ClientSecret,
            UserName = model.Login,
            Password = model.Password,
            Scope = "api"
        });

        if (tokenResponse.IsError)
        {
            throw new LogicException(ResultCode.IDENTITY_SERVER_ERROR);
        }

        return tokenResponse;
    }
}