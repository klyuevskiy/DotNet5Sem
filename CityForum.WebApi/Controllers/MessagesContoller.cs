using AutoMapper;
using CityForum.Services.Abstract;
using CityForum.Services.Models;
using CityForum.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityForum.WebApi.Controllers
{
    /// <summary>
    /// messages endpoints
    /// </summary>
    [ProducesResponseType(200)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messagesService;
        private readonly IMapper mapper;

        /// <summary>
        /// Users controller
        /// </summary>
        public MessagesController(IMessageService messagesService, IMapper mapper)
        {
            this.messagesService = messagesService;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateMessage([FromBody] CreateMessageRequest createMessageRequest)
        {
            var validationResult = createMessageRequest.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var resultModel = messagesService.CreateMessage(mapper.Map<CreateMessageModel>(createMessageRequest));
                return Ok(resultModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("{topicId}")]
        public IActionResult GetMessages([FromRoute] Guid topicId, [FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            try
            {
                return Ok(mapper.Map<PageResponse<MessageResponse>>(messagesService.GetMessages(topicId, limit, offset)));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteMessage([FromRoute] Guid id)
        {
            try
            {
                messagesService.DeleteMessage(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateMessage([FromRoute] Guid id, [FromBody] UpdateMessageRequest updateMessageRequest)
        {
            var validationResult = updateMessageRequest.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            try
            {
                var resultModel = messagesService.UpdateMessage(id, mapper.Map<UpdateMessageModel>(updateMessageRequest));
                return Ok(resultModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}