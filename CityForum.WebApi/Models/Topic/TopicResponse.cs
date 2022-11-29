namespace CityForum.WebApi.Models;

public class TopicResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? CreatedUserId { get; set; }
}