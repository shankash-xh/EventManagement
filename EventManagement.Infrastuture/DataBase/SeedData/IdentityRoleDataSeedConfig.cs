using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrastuture.DataBase.SeedData;

public class IdentityRoleDataSeedConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>
        {
            RoleId = "7406ced6-8b5d-4f64-9319-d2ed8a9cfbf3",
            UserId = "d664434f-5aff-4a91-b51a-38e1214c3f14"
        }, new IdentityUserRole<string>
        {
            RoleId = "c27c7cb2-29c0-4eaf-9507-f34baf66f299",
            UserId = "2ab08aff-4616-40db-8439-3e39478b20d5"
        });
    }
}
