using Microsoft.AspNetCore.Identity;

namespace CityForum.Entities.Models;

public class User : IdentityUser<Guid>, IBaseEntity
{
    public string Login { get; set; }

    public virtual ICollection<Topic> CreatedTopics { get; set; }
    public virtual ICollection<Message> SendingMessages { get; set; }

    #region BaseEntity

    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }

    public void Init()
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.UtcNow;
        ModificationTime = DateTime.UtcNow;
    }

    public bool IsNew()
    {
        return Id == Guid.Empty;
    }

    #endregion
}

public class UserRole : IdentityRole<Guid>
{

}