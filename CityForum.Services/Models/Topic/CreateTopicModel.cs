namespace CityForum.Services.Models;

public class CreateTopicModel
{
    public string Name { get; set; }
    public Guid CreatedUserId { get; set; }
}