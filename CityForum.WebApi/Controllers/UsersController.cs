using AutoMapper;
using CityForum.Services.Abstract;
using CityForum.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityForum.WebApi.Controllers
{
    /// <summary>
    /// Users endpoints
    /// </summary>
    [ProducesResponseType(200)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        /// <summary>
        /// Users controller
        /// </summary>
        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            try
            {
                userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            try
            {
                return Ok(mapper.Map<UserResponse>(userService.GetUser(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            try
            {
                return Ok(mapper.Map<PageResponse<UserResponse>>(userService.GetUsers(limit, offset)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}