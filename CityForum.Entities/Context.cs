using Microsoft.EntityFrameworkCore;
using CityForum.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CityForum.Entities;

public class Context : IdentityDbContext<User, UserRole, Guid>
{
    /*
    public DbSet<User> Users { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Message> Messages { get; set; }
    */

    public Context(DbContextOptions<Context> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Users

        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().HasKey(x => x.Id);
        builder.Entity<User>().HasIndex(x => x.Login)
                                .IsUnique();

        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        builder.Entity<UserRole>().ToTable("user_roles");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");

        #endregion

        #region Topics

        builder.Entity<Topic>().ToTable("topics");
        builder.Entity<Topic>().HasKey(x => x.Id);
        builder.Entity<Topic>().HasIndex(x => x.Name)
                                .IsUnique();

        // one-to-many User-Topic
        builder.Entity<Topic>().HasOne(x => x.CreatedUser)
                                .WithMany(x => x.CreatedTopics)
                                .HasForeignKey(x => x.CreatedUserId)
                                .OnDelete(DeleteBehavior.SetNull);

        #endregion

        #region Messages

        builder.Entity<Message>().ToTable("messages");
        builder.Entity<Message>().HasKey(x => x.Id);

        // one-to-many Topic-Message
        builder.Entity<Message>().HasOne(x => x.Topic)
                                    .WithMany(x => x.Messages)
                                    .HasForeignKey(x => x.TopicId)
                                    .OnDelete(DeleteBehavior.Cascade);

        // one-to-many User-Message
        builder.Entity<Message>().HasOne(x => x.SendingUser)
                                    .WithMany(x => x.SendingMessages)
                                    .HasForeignKey(x => x.SendingUserId)
                                    .OnDelete(DeleteBehavior.Cascade);

        #endregion
    }
}