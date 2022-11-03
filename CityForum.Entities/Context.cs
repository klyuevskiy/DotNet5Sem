using Microsoft.EntityFrameworkCore;
using CityForum.Entities.Models;

namespace CityForum.Entities;

public class Context : DbContext
{
    public DbSet<User> Users {get; set;}
    public DbSet<Topic> Topics {get; set;}
    public DbSet<Message> Messages {get; set;}

    public Context(DbContextOptions<Context> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region Users

        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().HasKey(x => x.Id);

        #endregion

        #region Topics

        builder.Entity<Topic>().ToTable("topics");
        builder.Entity<Topic>().HasKey(x => x.Id);
        // one-to-many User-Message
        builder.Entity<Topic>().HasOne(x => x.CreatedUser)
                                .WithMany(x => x.CreatedTopics)
                                .HasForeignKey(x => x.CreatedUserId)
                                .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region Messages
        
        builder.Entity<Message>().ToTable("messages");
        builder.Entity<Message>().HasKey(x => x.Id);
        // one-to-many User-Message
        builder.Entity<Message>().HasOne(x => x.SendingUser)
                                    .WithMany(x => x.SendingMessages)
                                    .HasForeignKey(x => x.SendingUserId)
                                    .OnDelete(DeleteBehavior.Cascade);
        // one-to-many Topic-Message
        builder.Entity<Message>().HasOne(x => x.Topic)
                                    .WithMany(x => x.Messages)
                                    .HasForeignKey(x => x.TopicId)
                                    .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}