namespace CityForum.WebApi.Models;

public class CreateTopicRequest : TopicRequest
{
    public Guid CreatedUserId { get; set; }
}