namespace CityForum.Services.Models;

public class MessageModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid SendingUserId { get; set; }
    public Guid TopicId { get; set; }
}