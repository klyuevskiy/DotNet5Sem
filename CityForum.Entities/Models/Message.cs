namespace CityForum.Entities.Models;

public class Message : BaseEntity
{
    public string Text {get; set;}
    
    public Guid TopicId {get; set;}
    public virtual Topic Topic {get; set;}

    public Guid SendingUserId {get; set;}
    public virtual User SendingUser {get; set;}
}