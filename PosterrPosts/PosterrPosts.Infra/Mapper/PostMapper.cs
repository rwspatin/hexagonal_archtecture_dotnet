using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosterrPosts.Domain.Entities;

namespace PosterrPosts.Infra.Mapper
{
    public class PostMapper : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .ToTable("TB_POST")
                .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID_POST").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.PostText).HasColumnName("POST_TEXT").HasMaxLength(777).IsRequired();
            builder.Property(x => x.UserId).HasColumnName("USER_ID").IsRequired();
            builder.Property(x => x.PostId).HasColumnName("POST_RELATED_ID");
            builder.Property(x => x.PostType).HasColumnName("POST_TYPE").IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Posts);

            builder
                .HasMany(x => x.SubPosts)
                .WithOne()
                .HasForeignKey(x => x.PostId);
        }
    }
}
