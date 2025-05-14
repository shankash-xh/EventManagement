using EventManagement.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrastuture.DataBase.SeedData;

public class UserSeedData : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();
        builder.HasData(new User
        {
            Id = "d664434f-5aff-4a91-b51a-38e1214c3f14",
            UserName = "Organizers",
            Email = "Organizers@gmail.com",
            NormalizedEmail = "admin@yopmail.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "Organizers@123"),
            NormalizedUserName = "Organizers".ToUpper(),
            EmailConfirmed = true,
            RefeshToken = null,
            RefeshTokenExpiryTime = null
        },
        new User
        {
            Id = "2ab08aff-4616-40db-8439-3e39478b20d5",
            UserName = "User",
            Email = "user@yopmail.com",
            NormalizedEmail = "user@gmail.com".ToUpper(),
            PasswordHash = hasher.HashPassword(null, "User@123"),
            NormalizedUserName = "User".ToUpper(),
            EmailConfirmed = true,
            RefeshToken = null,
            RefeshTokenExpiryTime = null
        });
    }
}
