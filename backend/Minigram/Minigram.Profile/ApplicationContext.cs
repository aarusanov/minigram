namespace Minigram.Profile
{
    using Microsoft.EntityFrameworkCore;
    using Minigram.Core.Context;
    using Minigram.Profile.Models;

    public class ApplicationContext : BaseDbContext
    {
        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Relation> Relationships { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relation>()
                .Navigation(r => r.Sender)
                .AutoInclude();
            
            modelBuilder.Entity<Relation>()
                .Navigation(r => r.Receiver)
                .AutoInclude();
        }
    }
}