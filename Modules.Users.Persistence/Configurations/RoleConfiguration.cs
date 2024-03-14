using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Roles;
using Modules.Users.Persistence.Constants;

namespace Modules.Users.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="Role"/> entity configuration.
/// </summary>
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        ConfigureDataStructure(builder);
    }
    
    private static void ConfigureDataStructure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Id).ValueGeneratedNever();

        builder.Property(role => role.Name).HasMaxLength(100);

        builder.HasData(Role.GetValues());
    }
    
    private static void ConfigureRelationships(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(role => role.Permissions)
            .WithMany(permission => permission.Roles)
            .UsingEntity<RolePermission>();
    }

}