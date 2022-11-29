namespace CityForum.WebApi.Models;

public class CreateMessageRequest : MessageRequest
{
    public Guid TopicId { get; set; }
    public Guid SendingUserId { get; set; }
}