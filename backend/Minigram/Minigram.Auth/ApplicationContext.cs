namespace Minigram.Auth
{
    using Microsoft.EntityFrameworkCore;
    using Minigram.Core.Context;
    using Minigram.Auth.Models;

    public class ApplicationContext : BaseDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshSession> RefreshSessions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) {}
    }
}