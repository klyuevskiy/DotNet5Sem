namespace CityForum.Services.Models;

public class TopicModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? CreatedUserId { get; set; }
}