using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Infrastuture.DataBase.SeedData;

public class RoleSeedData : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(new IdentityRole
        {
            Id = "7406ced6-8b5d-4f64-9319-d2ed8a9cfbf3",
            Name = "Organizers",
            NormalizedName = "Organizers".ToUpper(),
            ConcurrencyStamp = "7406ced6-8b5d-4f64-9319-d2ed8a9cfbf3"
        },
     new IdentityRole
     {
         Id = "c27c7cb2-29c0-4eaf-9507-f34baf66f299",
         Name = "User",
         NormalizedName = "User".ToUpper(),
         ConcurrencyStamp = "c27c7cb2-29c0-4eaf-9507-f34baf66f299"
     });
    }
}

