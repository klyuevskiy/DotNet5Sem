using AutoMapper;
using CityForum.Services.Abstract;
using CityForum.Services.Models;
using CityForum.WebApi.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityForum.WebApi.Controllers
{
    /// <summary>
    /// Auth endpoints
    /// </summary>
    [ProducesResponseType(200)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        /// <summary>
        /// Auth controller
        /// </summary>
        public AuthController(IAuthService authService, IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                RegisterUserModel registerModel = mapper.Map<RegisterUserModel>(request);
                UserModel result = await authService.RegisterUser(registerModel);
                UserResponse response = mapper.Map<UserResponse>(result);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserRequest request)
        {
            var validationResult = request.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                LoginUserModel loginModel = mapper.Map<LoginUserModel>(request);
                TokenResponse response = await authService.LoginUser(loginModel);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}