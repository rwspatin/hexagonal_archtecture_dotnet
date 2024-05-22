using Microsoft.EntityFrameworkCore;
using PosterrPosts.Domain.Entities;

namespace PosterrPosts.Infra
{
    public class PosterrPostDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public PosterrPostDbContext(DbContextOptions<PosterrPostDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PosterrPostDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
