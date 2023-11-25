using MethodicalSupportDisciplines.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MethodicalSupportDisciplines.Infrastructure.DatabaseContext.EntityConfigurations.Users;

public class GuestUserEntityConfiguration : IEntityTypeConfiguration<GuestUser>
{
    public void Configure(EntityTypeBuilder<GuestUser> builder)
    {
        builder.HasKey(property => property.GuestUserId);

        builder.Ignore(property => property.FirstName);
        builder.Ignore(property => property.LastName);
        builder.Ignore(property => property.Patronymic);
    }
}