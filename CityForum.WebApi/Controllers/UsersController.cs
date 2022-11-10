using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using CityForum.Entities.Models;
using CityForum.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CityForum.WebApi.Controllers
{
    /// <summary>
    /// Doctors endpoints
    /// </summary>
    [ProducesResponseType(200)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IRepository<User> _repository;

        /// <summary>
        /// Users controller
        /// </summary>
        public UsersController(IRepository<User> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var u1 = new User()
            {
                Login = "Alasld",
                PasswordHash = "125"
            };

            var u2 = new User()
            {
                Login = "ASDLl",
                PasswordHash = "1111"
            };

            try
            {
                u1 = _repository.Save(u1);
                u2 = _repository.Save(u2);
                u1.PasswordHash = "aslkdjlksadjl";
                u2.PasswordHash = "uiqe78213ehadsls";
                u1 = _repository.Save(u1);
                u2 = _repository.Save(u2);
            }
            catch (Exception e)
            {

            }

            var users = _repository.GetAll();

            return Ok(users);
        }
    }
}