namespace CityForum.Services.Models;

public class CreateMessageModel
{
    public string Text { get; set; }
    public Guid TopicId { get; set; }
    public Guid SendingUserId { get; set; }
}