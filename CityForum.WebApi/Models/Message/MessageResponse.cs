namespace CityForum.WebApi.Models;

public class MessageResponse
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid SendingUserId { get; set; }
    public Guid TopicId { get; set; }
}