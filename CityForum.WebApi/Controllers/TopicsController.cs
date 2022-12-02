using AutoMapper;
using CityForum.Services.Abstract;
using CityForum.Services.Models;
using CityForum.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityForum.WebApi.Controllers
{
    /// <summary>
    /// topics endpoints
    /// </summary>
    [ProducesResponseType(200)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService topicService;
        private readonly IMapper mapper;

        /// <summary>
        /// Users controller
        /// </summary>
        public TopicController(ITopicService topicService, IMapper mapper)
        {
            this.topicService = topicService;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateTopic([FromBody] CreateTopicRequest createTopicRequest)
        {
            var validationResult = createTopicRequest.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var resultModel = topicService.CreateTopic(mapper.Map<CreateTopicModel>(createTopicRequest));
                return Ok(resultModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTopic([FromRoute] Guid id)
        {
            try
            {
                return Ok(mapper.Map<TopicResponse>(topicService.GetTopic(id)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetTopics([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            try
            {
                return Ok(mapper.Map<PageResponse<TopicResponse>>(topicService.GetTopics(limit, offset)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTopic([FromRoute] Guid id)
        {
            try
            {
                topicService.DeleteTopic(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTopic([FromRoute] Guid id, [FromBody] UpdateTopicRequest updateTopicRequest)
        {
            var validationResult = updateTopicRequest.Validate();
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            try
            {
                var resultModel = topicService.UpdateTopic(id, mapper.Map<UpdateTopicModel>(updateTopicRequest));
                return Ok(resultModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}