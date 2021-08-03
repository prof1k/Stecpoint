namespace SIS.Second.Context.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SIS.Domain.Model.Entity;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(it => it.Phone).IsUnique();
            builder.HasIndex(it => it.Email).IsUnique();
        }
    }
}