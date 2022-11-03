namespace CityForum.Entities.Models;

public class User : BaseEntity
{
    public string Login {get; set;}
    public string PasswordHash {get; set;}

    public virtual ICollection<Topic> CreatedTopics {get; set;}
    public virtual ICollection<Message> SendingMessages {get; set;}
}