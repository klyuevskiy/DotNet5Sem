namespace CityForum.Entities.Models;

public class Topic : BaseEntity
{
    public string Name { get; set; }

    public Guid? CreatedUserId { get; set; }
    public virtual User? CreatedUser { get; set; }

    public virtual ICollection<Message> Messages { get; set; }
}