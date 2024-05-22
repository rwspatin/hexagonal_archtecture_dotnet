using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosterrPosts.Domain.Entities;

namespace PosterrPosts.Infra.Mapper
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("TB_USER")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_USER").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.UserName).HasColumnName("USER_NAME").HasMaxLength(14).IsRequired();
            builder.Property(x => x.CreationDate).HasColumnName("CREATED_DATE").IsRequired();

            builder
                .HasMany(x => x.Posts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
